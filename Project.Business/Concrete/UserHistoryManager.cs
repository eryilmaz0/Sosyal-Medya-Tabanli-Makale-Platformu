using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Project.Business.Abstract;
using Project.Business.Messages;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Business.BusinessResultObjects.Enums;
using Project.Core.Utilities.Business;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Concrete
{
    public class UserHistoryManager : IUserHistoryService
    {
        private readonly IProjectUnitOfWork _uow;
        private readonly IAuthService _authService;
        


        //DI
        public UserHistoryManager(IProjectUnitOfWork uow, IAuthService authService)
        {
            _uow = uow;
            _authService = authService;
            
        }




        public UserHistory CreateUserHistory(long articleCategoryId)
        {
            return new UserHistory(){ArticleCategoryId = articleCategoryId};
        }




        public UserHistory CreateUserHistory(long userId, long articleCategoryId)
        {
            return new UserHistory() { ArticleCategoryId = articleCategoryId, UserId = userId};
        }
    }
}