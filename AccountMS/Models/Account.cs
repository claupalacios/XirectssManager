using System;
using System.Collections.Generic;

#nullable disable

namespace AccountMS.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
    }
}
