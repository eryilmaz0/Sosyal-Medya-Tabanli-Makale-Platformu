using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.Core.Utilities.Paging;
using Project.Entities.Dtos;
using Project.Entities.Dtos.PostDtos;

namespace Project.Business.Abstract
{
    public interface IFavoriteService
    {
        IDataResult<PagedList<FavoriteDto>> GetFavoriteArticlesByUser(int currentPage, int pageSize);
        IResult CreateFavoriteArticle(AddFavoriteDto addFavoriteDto);
        IResult RemoveFavoriteArticle(long favoriteId);

    }
}
