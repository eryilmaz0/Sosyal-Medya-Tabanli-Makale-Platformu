using Project.Core.Entities;

namespace Project.Entities.Dtos.PostDtos
{
    public class AddTopicDto : DtoBase
    {
        public string Title { get; set; }
        public string Content { get; set; }

    }
}