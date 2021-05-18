using System;
using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.WebAPI.Dtos
{
    public class RegisterDto : DtoBase
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}