using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BookMall.Domain.Entities.User;

namespace BookMall.BuisnessLogic.Interfaces
{
    public interface ISession
    {
        PostResponse UserLogin(ULoginData data);
        PostResponse UserSignup(USignupData data);
        PostResponse UserChangeData(USettingsData data);
        string GenUserCookie(ULoginData data);
        HttpCookie GenCookie(string loginCredential);
        UProfileData GetUserByCookie(string apiCookieValue);
    }
}
