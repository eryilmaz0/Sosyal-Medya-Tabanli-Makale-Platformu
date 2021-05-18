using System;
using Project.Core.Entities;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Dtos
{
    public class UserHistoryDto : DtoBase
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long? ArticleId { get; set; }
        public long? ArticleCommentId { get; set; }
        public long? TopicId { get; set; }
        public long? TopicCommentId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
        public ArticleDto Article { get; set; }
        public ArticleCommentDto ArticleComment { get; set; }
        public TopicDto Topic { get; set; }
        public TopicCommentDto TopicComment { get; set; }
    }
}