using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class UpdateArticleCoverPhotoDto : DtoBase
    {
        public long? ArticleId { get; set; }
        public string CoverPhoto { get; set; }
    }
}