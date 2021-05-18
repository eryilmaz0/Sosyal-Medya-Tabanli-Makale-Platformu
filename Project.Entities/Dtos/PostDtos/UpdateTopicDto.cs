using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class UpdateTopicDto : AddTopicDto
    {
        public long TopicId { get; set; }
       
    }
}