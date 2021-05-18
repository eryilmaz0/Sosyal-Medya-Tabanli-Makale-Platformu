using System;
using System.Collections.Generic;
using Project.Core.Entities;
using Project.Entities.Entities;

namespace Project.Entities.Dtos
{
    public class ArticleDto : DtoBase
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        public long ArticleCategoryId { get; set; }
        public string ArticleFile { get; set; }
        public string Picture { get; set; }
        public long ViewCount { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }

        //NAV PROPS
        public UserDto User { get; set; }
        public ArticleCategoryDto ArticleCategory { get; set; }
        public List<FavoriteDto> Favorites { get; set; }
        public List<ArticleCommentDto> ArticleComments { get; set; }
        public List<ArticleLikeDisslikeDto> ArticleLikeDisslikes { get; set; }
        public List<UserHistoryDto> UserHistories { get; set; }


    }
}
