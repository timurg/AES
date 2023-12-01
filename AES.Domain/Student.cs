using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace AES.Domain
{
    
    /// <summary>
    /// Информация об обучающемся
    /// </summary>
    public sealed class Student : DomainObject
    {
        private Person person;


        /// <summary>
        /// Уникальное обозначение обучающегося
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string AgreementNumber { get; set; }

        /// <summary>
        /// Дата заключения договора обучающегося по направлению
        /// </summary>
        [Required]
        public System.DateTimeOffset AgreementDate { get; set; }

        /// <summary>
        /// Флаг активности обучающегося, если true - обучающийся имеет доступ к обучению
        /// </summary>
        [Required]
        public bool ActiveAgreement { get; set; }

        /// <summary>
        /// Номер стадии обучения, если 0, то обучающийся ещё в стадии незачисленного на обучение
        /// </summary>
        [Required]
        public byte Semester { get; set; }

        /// <summary>
        /// Дата начала текущей стадии обучения
        /// </summary>
        [Required]
        public System.DateTimeOffset WhenSemesterBegin { get; set; }

        /// <summary>
        /// Флаг, указывающий, что для обуающегося предусмотрена альтернативная траектория
        /// </summary>
        [Required]
        public bool MaybeAlternateRule { get; set; }

        /// <summary>
        /// Комментарий к договору обучающегося
        /// </summary>
        [MaxLength(255)]
        public string AgreementComment { get; set; }

        /// <summary>
        /// Подразделение в котором проходит обучение
        /// </summary>
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
        public Person Person { get; set; }

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
