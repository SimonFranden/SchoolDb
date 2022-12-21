using System;
using System.Collections.Generic;

namespace SchoolDb.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int FkSubjectId { get; set; }
        public int Grade1 { get; set; }
        public int FkStudentId { get; set; }
        public int FkStaffId { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual staff FkStaff { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
    }
}
