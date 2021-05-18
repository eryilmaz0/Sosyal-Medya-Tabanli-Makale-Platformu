using System;
using System.Collections.Generic;
using Project.Core.Entities;
using Project.Entities.Entities;

namespace Project.Entities.Dtos
{
    public class ChatDto : DtoBase
    {
        public long Id { get; set; }
        public long FirstUserId { get; set; }
        public long SecondUserId { get; set; }
        public ChatComment FirstComment { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto FirstUser { get; set; }
        public UserDto SecondUser { get; set; }
        public List<ChatCommentDto> ChatComments { get; set; }
    }
}
