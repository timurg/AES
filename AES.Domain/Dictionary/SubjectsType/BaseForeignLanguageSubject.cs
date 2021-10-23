using System;
using System.Collections.Generic;
using System.Text;

namespace AES.Domain
{
    public class BaseForeignLanguageSubject : Subject
    {
        public ICollection<LangugetSubject> LangugetSubjects { get; set; } = new HashSet<LangugetSubject>();
    }
}
