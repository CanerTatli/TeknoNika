using System;
using System.Collections.Generic;

namespace TeknoNikaWA
{
    public partial class Employee
    {
        public Employee()
        {
            Quota = new HashSet<Quota>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        public string? Title { get; set; }

        public virtual ICollection<Quota> Quota { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
