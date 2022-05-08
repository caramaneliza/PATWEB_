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
        public PostResponse UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }
        public PostResponse UserSignup(USignupData data)
        {
            return UserSignupAction(data);
        }
        public PostResponse UserChangeData(USettingsData data)
        {
            return UserChangeDataActioin(data);
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
