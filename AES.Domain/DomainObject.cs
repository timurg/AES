using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    /// <summary>
    /// Объект, от которого наследуются все остальные объекты
    /// </summary>
    public abstract class DomainObject
    {
        protected DomainObject()
        {
        }

        /// <summary>
        /// Базовый конструктор с инициализацией идентификатора
        /// </summary>
        /// <param name="nID">Идентификатор</param>
        protected DomainObject(System.Guid nID)
        {
            Id = nID;
        }

        /// <summary>
        /// Идентификатор объекта
        /// </summary>
        [Key]
        [Required]
        public System.Guid Id { get; set; }
    }
}

