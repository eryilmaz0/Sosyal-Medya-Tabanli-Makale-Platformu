using System.Collections.Generic;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.Core.Utilities.Paging;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class TopicManager : ITopicService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly IUserHistoryService _userHistoryService;
       


        //DI
        public TopicManager(IAuthService authService, IProjectUnitOfWork uow, IUserHistoryService userHistoryService)
        {
            _authService = authService;
            _uow = uow;
            _userHistoryService = userHistoryService;
            
        }



        public IDataResult<PagedList<TopicDto>> GetAllTopics(int currentPage, int pageSize)
        {
            var errorResult = BusinessRules.Run(ValidatePagingParams(currentPage, pageSize));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<PagedList<TopicDto>>(errorResult);
            }

            var pagedTopicsList = PagedList<TopicDto>.ToPagedList(_uow.Topics.GetAllTopicsWithInclude(), currentPage, pageSize);
            return new SuccessDataResult<PagedList<TopicDto>>(pagedTopicsList);
        }






        public IDataResult<TopicDto> GetTopic(long topicId)
        {
            var errorResult = BusinessRules.Run(IsTopicExist(topicId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<TopicDto>(errorResult);
            }
            

            return new SuccessDataResult<TopicDto>(this._uow.Topics.GetTopicWithInclude(topicId));
        }






        public IResult CreateTopic(AddTopicDto addTopicDto)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist());

            if (errorResult != null)
            {
                return errorResult;
            }
            
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var topic = new Topic(){Title = addTopicDto.Title, Content = addTopicDto.Content, UserId = user.Id};
            _uow.Topics.Add(topic);
            _uow.Commit();

           return new SuccessResult(Message.TopicCreated);
        }






        public IResult UpdateTopic(UpdateTopicDto updateTopicDto)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsTopicExist(updateTopicDto.TopicId),
                                                IsTopicBelongToUser(user.Id,updateTopicDto.TopicId));

            if (errorResult != null)
            {
                return errorResult;
            }

            var topic = _uow.Topics.Get(x => x.Id == updateTopicDto.TopicId);

            topic.Content = updateTopicDto.Content;
            topic.Title = updateTopicDto.Title;

            
            _uow.Topics.Update(topic);
            _uow.Commit();

            return new SuccessResult(Message.TopicUpdated);
        }






        public IResult RemoveTopic(long topicId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(),IsTopicExist(topicId), 
                                                IsTopicBelongToUser(user.Id, topicId));

            if (errorResult != null)
            {
                return errorResult;
            }
            
            var topic = _uow.Topics.Get(x => x.Id == topicId);
            _uow.Topics.Remove(topic);
            _uow.Commit();

            return new SuccessResult(Message.TopicDeleted);
        }







        public IDataResult<PagedList<TopicCommentDto>> GetTopicCommentsByTopic(long topicId, int currentPage, int pageSize)
        {
           var errorResult = BusinessRules.Run(IsTopicExist(topicId), ValidatePagingParams(currentPage, pageSize));

          if(errorResult != null)
          {
            return ResultConverter.ResultToDataResult<PagedList<TopicCommentDto>>(errorResult);
          }

          var pagedTopicComments = PagedList<TopicCommentDto>.ToPagedList(_uow.TopicComments.GetTopicCommentsByTopic(topicId), currentPage, pageSize);
          return new SuccessDataResult<PagedList<TopicCommentDto>>(pagedTopicComments);
        }






        public IDataResult<TopicCommentDto> GetTopicCommentById(long topicCommentId)
        {
            var errorResult = BusinessRules.Run(IsTopicCommentExist(topicCommentId));

            if (errorResult != null)
            {
                return ResultConverter.ResultToDataResult<TopicCommentDto>(errorResult);
            }

            return new SuccessDataResult<TopicCommentDto>(_uow.TopicComments.GetTopicCommentById(topicCommentId));
        }







        public IResult CreateTopicComment(AddTopicCommentDto addTopicCommentDto)
        {
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(),IsTopicExist(addTopicCommentDto.TopicId));

            if (errorResult != null)
            {
                return errorResult;
            }
            
            var user = _authService.GetAuthenticatedUser().Result.Data;
            var topic = _uow.Topics.Get(x => x.Id == addTopicCommentDto.TopicId);

            var topicComment = new TopicComment(){UserId = user.Id, Comment = addTopicCommentDto.Comment};
            topic.TopicComments.Add(topicComment);
            _uow.Topics.Update(topic);
            _uow.Commit();

            return new SuccessResult(Message.TopicCommentCreated);
        }






        public IResult UpdateTopicComment(UpdateTopicCommentDto updateTopicCommentDto)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;
            
            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(),IsTopicCommentExist(updateTopicCommentDto.TopicCommentId), 
                                                IsTopicCommentBelongToUser(user.Id, updateTopicCommentDto.TopicCommentId));

            if (errorResult != null)
            {
                return errorResult;
            }

            var topicComment = _uow.TopicComments.Get(x => x.Id == updateTopicCommentDto.TopicCommentId);

            topicComment.Comment = updateTopicCommentDto.Comment;
            _uow.TopicComments.Update(topicComment);
            _uow.Commit();

            return new SuccessResult(Message.TopicCommentUpdated);
        }





        public IResult RemoveTopicComment(long topicCommentId)
        {
            var user = _authService.GetAuthenticatedUser().Result.Data;

            var errorResult = BusinessRules.Run(CheckAuthenticatedUserExist(), IsTopicCommentExist(topicCommentId),
                                                IsTopicCommentBelongToUser(user.Id, topicCommentId));

            if (errorResult != null)
            {
                return errorResult;
            }
            
            var topicComment = _uow.TopicComments.Get(x => x.Id == topicCommentId);
            _uow.TopicComments.Remove(topicComment);
            _uow.Commit();

            return new SuccessResult(Message.TopicCommentRemoved);

        }





        //

        private IResult CheckAuthenticatedUserExist()
        {

            var user = _authService.GetAuthenticatedUser().Result.Data;

            if (user == null)
            {
                return new UnauthorizedResult();
            }

            return new SuccessResult();

        }






        private IResult IsTopicExist(long topicId)
        {

            var topic = _uow.Topics.Get(x => x.Id == topicId);

            if (topic == null)
            {
                return new ErrorResult(Message.TopicNotFound);
            }

            return new SuccessResult();

        }





        private IResult IsTopicBelongToUser(long userId, long topicId)
        {

            var topic = _uow.Topics.Get(x => x.Id == topicId);

            if (topic.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnTopic);
            }

            return new SuccessResult();

        }





        private IResult IsTopicCommentExist(long topicCommentId)
        {
            var topicComment = _uow.TopicComments.Get(x => x.Id == topicCommentId);

            if (topicComment == null)
            {
                return new ErrorResult(Message.TopicCommentNotFound);
            }

            return new SuccessResult();
        }




        private IResult IsTopicCommentBelongToUser(long userId, long topicCommentId)
        {
            var topicComment = _uow.TopicComments.Get(x => x.Id == topicCommentId);

            if (topicComment.UserId != userId)
            {
                return new ErrorResult(Message.CantProcessOnArticleComment);
            }

            return new SuccessResult();
        }




        private IResult ValidatePagingParams(int currentPage, int pageSize)
        {
            if (currentPage <= 0 || pageSize <= 0)
            {
                return new ErrorResult(Message.PagingParamsNotValid);
            }

            return new SuccessResult();
        }
    }
}
