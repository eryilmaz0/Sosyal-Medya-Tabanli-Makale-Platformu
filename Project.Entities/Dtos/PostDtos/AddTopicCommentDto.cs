using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class AddTopicCommentDto : DtoBase
    {
        public long TopicId { get; set; }
        public string Comment { get; set; }
    }
}