using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;

namespace Project.DataAccess.Abstract
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        List<FavoriteDto> GetFavoriteArticlesByUser(long userId);
    }
}