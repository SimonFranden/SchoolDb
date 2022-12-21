using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Grades = new HashSet<Grade>();
        }

        public int SubjectId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
