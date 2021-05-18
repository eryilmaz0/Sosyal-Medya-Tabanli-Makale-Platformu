using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface ITopicCommentRepository : IRepository<TopicComment>
    {
        TopicCommentDto GetTopicCommentById(long topicCommentId);
        List<TopicCommentDto> GetTopicCommentsByTopic(long topicId);
    }
}
