using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMall.Domain.Entities.User;

namespace BookMall.BuisnessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        ULoginResp UserSignup(ULoginData data);
        string GenUserCookie(ULoginData data);
    }
}
