using System;
using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Dtos
{
    public class AuthorizationAppealDto : DtoBase
    {
        public long Id { get; set; }
        public string AppealDescription { get; set; }
        public long UserId { get; set; }
        public AppealStatus AppealStatus { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
    }
}