using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class StaffRole
    {
        public StaffRole()
        {
            staff = new HashSet<staff>();
        }

        public int StaffRoleId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<staff> staff { get; set; }
    }
}
