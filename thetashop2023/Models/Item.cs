using System;
using System.Collections.Generic;

namespace thetashop2023.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? Images { get; set; }
        public string? Status { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
