using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AES.Domain
{
    public class LangugetSubject : Subject
    {
        [Required]
        public Language Language { get; set; }

        [Required]
        public BaseForeignLanguageSubject BaseSubject { get; set; }
    }
}
