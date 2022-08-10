using System;

namespace UserMS.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active = true;
    }
}
