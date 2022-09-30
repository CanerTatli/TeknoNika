using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class ProductSale
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? SalesOrderId { get; set; }
        public int? Quantity { get; set; }
        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Product? Product { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }
    }
}
