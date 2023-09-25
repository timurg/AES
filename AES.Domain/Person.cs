using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public class Person : DomainObject
    {

        /// <summary>
        /// Имя персоны
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Фамилия персоны
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName
        {
            get; set;
        }

        /// <summary>
        /// Отчество персоны
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(100)]
        public string Patronymic
        {
            get; set;
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Login
        {
            get; set;
        }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(255)]
        public string Password
        {
            get; set;
        }

        /// <summary>
        /// Флаг активности персоны
        /// </summary>
        [Required]
        public bool Active
        {
            get; set;
        }

        /// <summary>
        /// Электронная почта
        /// </summary>
        [MaxLength(255)]
        public string Email
        {
            get; set;
        }

        /// <summary>
        /// Информация по студенту связанному с данной персоной
        /// </summary>
        public Student Student { get; set; }

        /// <summary>
        /// Информация по куратору связанному с данной персоной
        /// </summary>
        public Curator Curator { get; set; }

        /// <summary>
        /// Информация по тьюторству связанному с данной персоной
        /// </summary>
        public Tutor Tutor { get; set; }

        /// <summary>
        /// Время последней активности
        /// </summary>
        public DateTime? LastActivityDateTime
        {
            get; set;
        }

        /// <summary>
        /// Дата установки пароля
        /// </summary>
        public DateTime? WhenSetPassWord
        {
            get; set;
        }

        /// <summary>
        /// Пол
        /// </summary>
        [Required]
        public Sex Sex
        {
            get; set;
        }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Required]
        public DateTime Birthday
        {
            get; set;
        }

        [Required]
        public Guid PhotoID
        {
            get; set;
        }


        private Person() : base(Guid.Empty)
        {

        }


        public Person(Guid nId) : base(nId)
        {
            Roles = new HashSet<Role>();
        }

        public ICollection<Role> Roles
        {
            get; set;
        }

        /// <summary>
        /// Полное имя персоны
        /// </summary>
        public string FullName => $"{LastName} {Name} {Patronymic}";
    }
}
