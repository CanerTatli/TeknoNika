using System;
using System.Collections.Generic;

namespace TeknoNikaWA
{
    public partial class Customer
    {
        public Customer()
        {
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? Gender { get; set; }
        public short? Age { get; set; }
        public int TcId { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
