using System;
using System.Collections.Generic;

namespace TeknoNikaWA
{
    public partial class Quota
    {
        public int Id { get; set; }
        public int? SalesQuota { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
    }
}
