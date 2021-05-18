using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface IAuthorizationAppealRepository : IRepository<AuthorizationAppeal>
    {
        AuthorizationAppealDto GetIncludedAppealById(long appealId);
    }
}