using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AES.Domain
{
    public class Curriculum : DomainObject
    {
        [Required]
        public Student Student { get; set; }
        public ISet<Module> Modules { get; set; } = new HashSet<Module>();

        [Required]
        public DateTime DateOfAppointment { get; set; }

        /// <summary>
        /// Тэг для поиска плана
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string tag { get; set; }
    }
}
