using System;
using System.Collections.Generic;

namespace thetashop2023.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
