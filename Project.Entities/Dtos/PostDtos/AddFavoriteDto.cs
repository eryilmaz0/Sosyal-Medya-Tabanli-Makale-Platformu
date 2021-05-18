using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class AddFavoriteDto : DtoBase
    {
        public string Description { get; set; }
        public long ArticleId { get; set; }
    }
}