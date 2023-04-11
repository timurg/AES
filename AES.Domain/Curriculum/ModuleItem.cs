﻿using System.ComponentModel.DataAnnotations;

namespace AES.Domain
{
    public abstract class ModuleItem : DomainObject
    {
        [Required]
        public bool IsRequared { get; set; }

        public GradeRecord Grade { get; set; }

        [Required]
        public Module Module { get; set; }

        [Required]
        public Subject Subject { get; set; }
        [Required]
        public TypeTesting TypeTesting { get; set; }

        [Required]
        public int Semester { get; set; }
        
        public LearningProcess LearningProcess { get; set; }
    }
}
