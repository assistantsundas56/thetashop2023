﻿using System;
using System.Collections.Generic;

namespace thetashop2023.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Status { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
