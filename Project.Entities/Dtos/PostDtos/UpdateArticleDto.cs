using System.Runtime.CompilerServices;

namespace Project.Entities.Dtos.PostDtos
{
    public class UpdateArticleDto : AddArticleDto
    {
        public long ArticleId { get; set; }
    }
}