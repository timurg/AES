using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    /// <summary>
    /// Модуль содержащий перечень дисциплин
    /// </summary>
    public abstract class Module : DomainObject
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public bool IsRequared { get; set; }
        public GradeRecord Grade { get; set; }
        [Required]
        public Curriculum Curriculum { get; set; }
        public ISet<ModuleItem> Items { get; set; } = new HashSet<ModuleItem>();
    }
}
