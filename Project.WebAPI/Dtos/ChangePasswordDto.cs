using Project.Core.Entities;

namespace Project.WebAPI.Dtos
{
    public class ChangePasswordDto : DtoBase
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }
    }
}