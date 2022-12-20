using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class staff
    {
        public staff()
        {
            Grades = new HashSet<Grade>();
        }

        public int StaffId { get; set; }
        public string Fname { get; set; } = null!;
        public string? Lname { get; set; }
        public int? FkStaffRoleId { get; set; }

        public virtual StaffRole? FkStaffRole { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
