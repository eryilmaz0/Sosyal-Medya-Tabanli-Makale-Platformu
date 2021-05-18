using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Paging;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;

namespace Project.Business.Abstract
{
    public interface ITopicService
    {
        IDataResult<PagedList<TopicDto>> GetAllTopics(int currentPage, int pageSize);
        IDataResult<TopicDto> GetTopic(long topicId);
        IResult CreateTopic(AddTopicDto addtopicDto);
        IResult UpdateTopic(UpdateTopicDto updateTopicDto);
        IResult RemoveTopic(long topicId);
        IDataResult<TopicCommentDto> GetTopicCommentById(long topicCommentId);
         IDataResult<PagedList<TopicCommentDto>> GetTopicCommentsByTopic(long topicId, int currentPage, int pageSize);
        IResult CreateTopicComment(AddTopicCommentDto addTopicCommentDto);
        IResult UpdateTopicComment(UpdateTopicCommentDto updateTopicCommentDto);
        IResult RemoveTopicComment(long topicCommentId);
    }
}
