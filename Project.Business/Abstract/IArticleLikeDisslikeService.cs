using System.Collections.Generic;
using Project.Core.Business.BusinessResultObjects;
using Project.DataAccess.UnitOfWork;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Business.Abstract
{
    public interface IArticleLikeDisslikeService
    {
        IDataResult<List<ArticleLikeDisslikeDto>> GetUsersLikedOrDisslikedArticle(long articleId, LikeDisslikeType likeDisslikeType);
        IResult AddLike(long articleId);
        IResult AddDisslike(long articleId);
        IResult DeleteLike(ArticleLikeDisslike userLike);
        IResult DeleteDisslike(ArticleLikeDisslike userLike);
    }
}