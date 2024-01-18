using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    
    /// <summary>
    /// Информация о роли тьютора пользователя
    /// </summary>
    public class Tutor : DomainObject
    {
        public ICollection<TutorDescription> Descriptions { get; set; } = new HashSet<TutorDescription>();

        [Required]
        public virtual Person Person { get; set; }
    }
}
