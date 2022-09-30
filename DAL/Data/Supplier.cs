using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? SupplyDate { get; set; }
        public int? SupplyQuantity { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
