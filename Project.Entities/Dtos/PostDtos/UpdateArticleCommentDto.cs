using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class UpdateArticleCommentDto : DtoBase
    {
        public long ArticleCommentId { get; set; }
        public string Comment { get; set; }
    }
}