using System;
using Project.Core.Entities;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Dtos
{
    public class ArticleLikeDisslikeDto : DtoBase
    {
        public long Id { get; set; }
        public LikeDisslikeType LikeDisslikeType { get; set; }
        public long ArticleId { get; set; }
        public long UserId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
        public ArticleDto Article { get; set; }
    }
}