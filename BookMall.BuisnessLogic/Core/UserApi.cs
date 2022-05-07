using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.User;
using BookMall.Domain.Entities.Product;
using eUseControl.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity; 
using BookMall.BuisnessLogic.DBModel;
using BookMall.Helpers;
using System.Web;

namespace BookMall.BuisnessLogic.Core
{
    public class UserApi
    {
        internal PostResponse UserLoginAction(ULoginData data)
        {

            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Credential))
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new PostResponse { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new PostResponse { Status = true };
            }
            else
            {
                var pass = LoginHelper.HashGen(data.Password);
                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Credential && u.Password == pass);
                }

                if (result == null)
                {
                    return new PostResponse { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new PostResponse { Status = true };
            }
        }

        internal PostResponse UserSignupAction(USignupData data)
        {

            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                if (data.Password1 == null || data.Email == null)
                {
                    return new PostResponse { Status = false, StatusMsg = "Complet all fields" };
                }

                if (data.Password1 != data.Password2)
                {
                    return new PostResponse { Status = false, StatusMsg = "The Passwords don't match" };
                }

                if (data.Password1.Length < 8)
                {
                    return new PostResponse { Status = false, StatusMsg = "Password min 8 characters" };
                }

                if (data.Username.Length < 5)
                {
                    return new PostResponse { Status = false, StatusMsg = "Username min 5 characters" };
                }

                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Email);
                }

                if (result != null)
                {
                    return new PostResponse { Status = false, StatusMsg = "The Email is already taken" };
                }

                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Username);
                }
                if (result != null)
                {
                    return new PostResponse { Status = false, StatusMsg = "please use a unique username" };
                }
                
                var pass = LoginHelper.HashGen(data.Password1);
                result = new UDbTable
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = pass,
                    LasIp = data.LoginIp,
                    FirstLogin = data.LoginDateTime,
                    LastLogin = data.LoginDateTime

                };

                using (var db = new UserContext())
                {
                    db.Users.Add(result);
                    db.SaveChanges();
                }

                return new PostResponse { Status = true };
            }
            else
            {
               //empty email is valid for some reason 
             return new PostResponse { Status = false, StatusMsg = "Invalid email" };
            
            }
        }
        internal PostResponse UserChangeDataActioin(USettingsData data)
        {
            UDbTable user;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                var pass = LoginHelper.HashGen(data.CurrentPassword);
                using (var db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Id == data.Id);
                }
                if (pass != user.Password)
                {
                    return new PostResponse { Status = false, StatusMsg = "Wrong password" };
                }

                return new PostResponse { Status = true };
            }
            else
            {
                //empty email is valid for some reason 
                return new PostResponse { Status = false, StatusMsg = "Invalid email" };

            }
        }
        internal List<ProductData> GetProductListByUser()
        {
            return new List<ProductData>();
        }

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("bm_token")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            //find email if username used
            UDbTable result;
            using (var db = new UserContext())
            {
                    result = db.Users.FirstOrDefault(u => u.Email == loginCredential || u.Username == loginCredential);
             }

            loginCredential = result.Email;


                using (var db = new UserContext())
            {
                SessionsDbTable curent;
                curent = (from e in db.Sessions where e.UserEmail == loginCredential select e).FirstOrDefault();
            
                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var up = new UserContext())
                    {
                        up.Entry(curent).State = EntityState.Modified;
                        up.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new SessionsDbTable
                    {
                        UserEmail = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }
        internal UProfileData UserCookie(string cookie)
        {
            SessionsDbTable session;
            UDbTable curentUser;

            using (var db = new UserContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.UserEmail))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.UserEmail);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.UserEmail);
                }
            }

            if (curentUser == null) return null;
            var userprofile = new UProfileData
            {
                Id = curentUser.Id,
                Username = curentUser.Username,
                Email = curentUser.Email,
                Level = curentUser.Level
            };

            return userprofile;
        }

    }
 
}
