using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface ITopicRepository : IRepository<Topic>
    {
        List<TopicDto> GetAllTopicsWithInclude();
        TopicDto GetTopicWithInclude(long topicId);
    }
}