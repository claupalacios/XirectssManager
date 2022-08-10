using System;
using System.Collections.Generic;

#nullable disable

namespace UserMS.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }
}
