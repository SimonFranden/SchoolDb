using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public string GradeSubject { get; set; } = null!;
        public string Grade1 { get; set; } = null!;
        public int FkStudentId { get; set; }
        public int FkStaffId { get; set; }

        public virtual staff FkStaff { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
