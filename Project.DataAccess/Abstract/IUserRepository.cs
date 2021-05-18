using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.DataAccess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        UserDto GetUserProfile(long userId);
        
    }
}