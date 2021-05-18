using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class AddArticleDto : DtoBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long ArticleCategoryId { get; set; }
    }
}