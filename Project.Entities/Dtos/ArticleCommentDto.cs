using System;
using System.Collections.Generic;
using Project.Core.Entities;

namespace Project.Entities.Dtos
{
    public class ArticleCommentDto : DtoBase
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public long UserId { get; set; }
        public long? ArticleId { get; set; }
        public bool? isDeleted { get; set; }


        //NAV PROPS
        public UserDto User { get; set; }
        public ArticleDto Article { get; set; }
        public List<UserHistoryDto> UserHistories { get; set; }
    }
}