using System;
using System.Collections.Generic;

namespace NIKA.DAL.Data
{
    public partial class ProductFile
    {
        public int? ProductId { get; set; }
        public int? LayoutTypeId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? IsDeleted { get; set; }
        public int? FileId { get; set; }

        public virtual File? File { get; set; }
        public virtual LayoutType? Product { get; set; }
        public virtual Product? ProductNavigation { get; set; }
    }
}
