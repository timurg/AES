using AES.Infrastructure;
using System;
using System.Linq;
using AES.Domain;

namespace AES.BusinessLogic.Implementation
{
    /// <summary>
    /// Класс для поиска пользователей
    /// </summary>
    public class UserFinderBO : BusinessObject, IUserFinder
    {
        public UserFinderBO(IUnitOfWork UnitOfWork) : base(UnitOfWork)
        {
        }

        public Person findByLogin(string login)
        {
            return (from person in UnitOfWork.PersonRepository.GetQuery()
                    where person.Login == login
                    select person).FirstOrDefault();
        }

        public Person findEmail(string email)
        {
            return (from person in UnitOfWork.PersonRepository.GetQuery()
                    where person.Email == email
                    select person).FirstOrDefault();
        }
    }
}
