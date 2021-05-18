using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface IUserHistoryRepository : IRepository<UserHistory>
    {
        //List<UserHistoryDto> GerUserHistories(long userId);
    }
}