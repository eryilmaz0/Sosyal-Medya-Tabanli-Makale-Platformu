using System.Threading.Tasks;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Security.JWT;
using Project.Entities.Entities;

namespace Project.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> GetAuthenticatedUser();
        Task<IResult> IsUserInRole(User user, string role);
        Task<IDataResult<User>> GetUser(long userId);
        Task<IDataResult<AccessToken>> GetAuthenticatedToken(User user);
    }
}