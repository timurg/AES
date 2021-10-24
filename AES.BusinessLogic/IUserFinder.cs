using AES.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AES.BusinessLogic
{
    public interface IUserFinder
    {
        Person findByLogin(string login);
        Person findEmail(string email);
    }
}
