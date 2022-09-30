using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class Quota
    {
        public int Id { get; set; }
        public int? SalesQuota { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
