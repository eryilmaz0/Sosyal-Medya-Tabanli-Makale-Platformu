using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class AddArticleCommentDto : DtoBase
    {
        public string Comment { get; set; }
        public long? ArticleId { get; set; }
    }
}