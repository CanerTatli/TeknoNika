using System;
using System.Collections.Generic;

namespace TeknoNikaWA
{
    public partial class ProductSale
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? SalesOrderId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual SalesOrder? SalesOrder { get; set; }
    }
}
