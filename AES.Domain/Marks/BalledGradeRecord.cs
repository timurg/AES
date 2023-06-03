using System;
using System.Collections.Generic;
using System.Text;

namespace AES.Domain
{
    public class BalledGradeRecord : GradeRecord
    {
        public override string Description
        {
            get
            {
                if (IsPassed)
                return "Зачтено";
                else
                {
                    return "Не зачтено";
                }
            }
        }
    }
}
