using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class UpdateTopicCommentDto : DtoBase
    {
        public long TopicCommentId { get; set; }
        public string Comment { get; set; }
    }
}