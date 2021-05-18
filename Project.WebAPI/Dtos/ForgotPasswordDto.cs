using Project.Core.Entities;

namespace Project.WebAPI.Dtos
{
    public class ForgotPasswordDto : DtoBase
    {
        public string Email { get; set; }
    }
}