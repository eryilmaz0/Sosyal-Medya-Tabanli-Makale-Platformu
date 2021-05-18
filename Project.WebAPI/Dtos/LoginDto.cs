using Project.Core.Entities;

namespace Project.WebAPI.Dtos
{
    public class LoginDto : DtoBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}