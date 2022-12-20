using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public int ClassId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
