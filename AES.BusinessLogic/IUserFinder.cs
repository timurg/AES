using AES.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AES.BusinessLogic
{
    /// <summary>
    /// Интерфейс для поиска пользователя по параметрам
    /// </summary>
    public interface IUserFinder
    {
        Person findByLogin(string login);
        Person findEmail(string email);
    }
}
