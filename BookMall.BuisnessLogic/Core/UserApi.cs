using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.User;
using BookMall.Domain.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity; 
using BookMall.BuisnessLogic.DBModel;
using BookMall.Helpers;


namespace BookMall.BuisnessLogic.Core
{
    public class UserApi
    {
        internal ULoginResp UserLoginAction(ULoginData data)
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
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
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
                    return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
                }

                using (var todo = new UserContext())
                {
                    result.LasIp = data.LoginIp;
                    result.LastLogin = data.LoginDateTime;
                    todo.Entry(result).State = EntityState.Modified;
                    todo.SaveChanges();
                }

                return new ULoginResp { Status = true };
            }
        }

        internal ULoginResp UserSignupAction(USignupData data)
        {

            UDbTable result;
            var validate = new EmailAddressAttribute();
            if (validate.IsValid(data.Email))
            {
                if (data.Password1 == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "Enter a PASSWORD" };
                }

                if (data.Email == null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "Consider using a email" };
                }

                if (data.Password1 != data.Password2)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Passwords don't match" };
                }

                if (data.Password1.Length < 8)
                {
                    return new ULoginResp { Status = false, StatusMsg = "Password min 8 characters" };
                }

                if (data.Username.Length < 5)
                {
                    return new ULoginResp { Status = false, StatusMsg = "Username min 5 characters" };
                }

                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Email == data.Email);
                }

                if (result != null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "The Email is already taken" };
                }

                using (var db = new UserContext())
                {
                    result = db.Users.FirstOrDefault(u => u.Username == data.Username);
                }
                if (result != null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "please use a unique username" };
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

                return new ULoginResp { Status = true };
            }
            else
            {
               
             return new ULoginResp { Status = false, StatusMsg = "Invalid email" };
            
            }
        }

        internal List<ProductData> GetProductListByUser()
        {
            return new List<ProductData>();
        }
         
    }
 
}
