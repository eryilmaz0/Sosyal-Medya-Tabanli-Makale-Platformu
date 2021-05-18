using System;
using System.Collections.Generic;
using Project.Core.Entities;

namespace Project.Entities.Dtos
{
    public class TopicDto : DtoBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
        public List<TopicCommentDto> TopicComments { get; set; }
        public List<UserHistoryDto> UserHistories { get; set; }
    }
}