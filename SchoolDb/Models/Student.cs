using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
        }

        public int StudentId { get; set; }
        public string Fname { get; set; } = null!;
        public string Lname { get; set; } = null!;
        public string SocialNum { get; set; } = null!;
        public int? FkClassId { get; set; }

        public virtual Class? FkClass { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
