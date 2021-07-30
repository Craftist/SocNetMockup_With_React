using SocNetMockup.Dtos;
using SocNetMockup.Models;

namespace SocNetMockup.DtoMappers
{
    public static class Mappers
    {
        public static readonly Mapper<User, UserDto> User = new(u => new() {
            Id = u.Id,
            UserName = u.UserName,
            RegDate = u.RegistrationDate
        });
    }
}
