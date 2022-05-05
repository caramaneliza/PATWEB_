using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BookMall.BuisnessLogic.Core;
using BookMall.BuisnessLogic.Interfaces;
using BookMall.Domain.Entities.User;
using BookMall.Helpers.Session;

namespace BookMall.BuisnessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            //LOGIN
            return UserLoginAction(data);
        }
        public ULoginResp UserSignup(USignupData data)
        {
            return UserSignupAction(data);
        }
        public string GenUserCookie(ULoginData data)
        {
            var session = new SessionActionType();
            var cookie = session.GenerateCookieBase(data.Credential);

            return cookie;
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }
        public UProfileData GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
