using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class Customer
    {
        public Customer()
        {
            ProductSales = new HashSet<ProductSale>();
            SalesOrders = new HashSet<SalesOrder>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public bool? Gender { get; set; }
        public short? Age { get; set; }
        public string TcId { get; set; } = null!;

        public virtual ICollection<ProductSale> ProductSales { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }
    }
}
