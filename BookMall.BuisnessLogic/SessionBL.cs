using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ULoginResp UserSignup(ULoginData data)
        {
            //LOGIN
            return UserSignupAction(data);
        }
        public string GenUserCookie(ULoginData data)
        {
            var session = new SessionActionType();
            var cookie = session.GenerateCookieBase(data.Credential);

            return cookie;
        }
    }
}
