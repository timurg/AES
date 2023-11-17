using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    /// <summary>
    /// Объект, от которого наследуются все остальные объекты
    /// </summary>
    public class DomainObject
    {
        /// <summary>
        /// Поле идентификатор объекта
        /// </summary>
        private System.Guid id;

        protected DomainObject()
        {
        }

        /// <summary>
        /// Базовый конструктор с инициализацией идентификатора
        /// </summary>
        /// <param name="nID">Идентификатор</param>
        public DomainObject(System.Guid nID)
        {
            id = nID;
        }

        [Key]
        [Required]
        public System.Guid Id { get => id; set => id = value; }
    }
}

