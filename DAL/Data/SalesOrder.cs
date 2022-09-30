using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? SaleDate { get; set; }
        public int? ProductId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
