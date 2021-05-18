using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;
using Project.Entities.Entities;

namespace Project.Business.Abstract
{
    public interface IAuthorizationAppealService
    {
        IDataResult<List<AuthorizationAppeal>> GetAuthorizationAppealsByUser();
        public IDataResult<AuthorizationAppealDto> GetAuthorizationAppealById(long appealId);
        IResult CreateAuthorizationAppeal(AddAuthorizationAppealDto addAuthorizationAppealDto);
    }
}