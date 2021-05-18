using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Abstract
{
    public interface IUserHistoryService
    {

        UserHistory CreateUserHistory(long articleCategoryId);
        UserHistory CreateUserHistory(long userId,long articleCategoryId);



        //UserHistory CreateUserHistory(long? userId = null);
        //UserHistory CreateArticleHistory(long userId, long? articleId = null);
        //UserHistory CreateArticleCommentHistory(long userId, long articleId, long? articleCommentId = null);
        //UserHistory CreateTopicHistory(long userId, long? topicId = null);
        //UserHistory CreateTopicCommentHistory(long userId, long topicId, long? topicCommentId = null);
        //IDataResult<List<UserHistoryDto>> GetUserHistories(long userId);


    }
}