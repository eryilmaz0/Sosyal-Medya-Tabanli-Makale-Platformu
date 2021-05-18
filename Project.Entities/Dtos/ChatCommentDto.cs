using System;
using Project.Core.Entities;

namespace Project.Entities.Dtos
{
    public class ChatCommentDto : DtoBase
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public long ChatId { get; set; }
        public long UserId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
        public ChatDto Chat { get; set; }
    }
}
