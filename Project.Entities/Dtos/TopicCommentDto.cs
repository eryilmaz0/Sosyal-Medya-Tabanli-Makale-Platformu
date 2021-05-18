using System;
using System.Collections.Generic;
using Project.Core.Entities;
using Project.Entities.Entities;

namespace Project.Entities.Dtos
{
    public class TopicCommentDto : DtoBase
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public long UserId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public TopicDto Topic { get; set; }
        public UserDto User { get; set; }
        public List<UserHistoryDto> UserHistories { get; set; }

    }
}
