using System;
using System.Collections.Generic;

namespace NIKA.DAL
{
    public partial class Product
    {
        public Product()
        {
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public bool? Discontinued { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UnitsSold { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }
    }
}
