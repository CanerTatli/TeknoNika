using System;
using System.Collections.Generic;

namespace TeknoNikaWA
{
    public partial class File
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? IsDeleted { get; set; }
    }
}
