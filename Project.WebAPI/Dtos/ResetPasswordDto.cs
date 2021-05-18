using Project.Core.Entities;

namespace Project.WebAPI.Dtos
{
    public class ResetPasswordDto : DtoBase
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public long? UserId { get; set; }
        public string ResetPasswordToken { get; set; }
    }
}