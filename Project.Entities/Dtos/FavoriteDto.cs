using System;
using Project.Core.Entities;

namespace Project.Entities.Dtos
{
    public class FavoriteDto : DtoBase
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }
        public string ArticleTitle { get; set; }

    //NAV PROPS
    public UserDto User { get; set; }
        public ArticleDto Article { get; set; }
    }
}
