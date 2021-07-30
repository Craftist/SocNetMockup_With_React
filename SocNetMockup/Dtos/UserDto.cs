using System;

namespace SocNetMockup.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public DateTime RegDate { get; set; }
    }
}
