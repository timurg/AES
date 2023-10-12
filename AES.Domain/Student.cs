using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AES.Domain
{
    
    public class Student : DomainObject
    {
        private Person person;


        [Required]
        [MaxLength(50)]
        public string AgreementNumber { get; set; }

        [Required]
        public System.DateTimeOffset AgreementDate { get; set; }

        [Required]
        public bool ActiveAgreement { get; set; }

        [Required]
        public byte Semester { get; set; }

        [Required]
        public System.DateTimeOffset WhenSemesterBegin { get; set; }

        [Required]
        public bool MaybeAlternateRule { get; set; }

        [MaxLength(255)]
        public string AgreementComment { get; set; }

        [Required]
        public Subdivision Subdivision { get; set; }

        [Required]
        public Direction Direction { get; set; }

        [Required]
        public Qualification Qualification { get; set; }

        [Required]
        public FormEducation FormEducation { get; set; }

        [Required]
        public Duration Duration { get; set; }

        [Required]
        public Specialization Specialization { get; set; }

        [Required]
        public Language StudiedLanguage { get; set; }

        public RateEducation BaseRateEducation { get; set; }

        public RateEducation EndRateEducation { get; set; }

        [Required]
        public virtual Person Person { get; set; }

        //[Required]
        public Curriculum Curriculum { get; set; }

        //public virtual ICollection<Course> Courses { get; set; }

        public Student()
            : this(System.Guid.Empty, null)
        {
        }

        public Student(Guid Id, Person person)
            : base(Id)
        {
            //Courses = new HashSet<Course>();
            this.person = person;
        }
    }
}
