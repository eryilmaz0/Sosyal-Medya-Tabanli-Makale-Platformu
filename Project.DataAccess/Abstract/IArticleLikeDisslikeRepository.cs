using System.Collections.Generic;
using Project.Core.DataAccess.Repository;
using Project.Entities.Dtos;
using Project.Entities.Entities;
using Project.Entities.EntityConfigurations;
using Project.Entities.Enums;

namespace Project.DataAccess.Abstract
{
    public interface IArticleLikeDisslikeRepository : IRepository<ArticleLikeDisslike>
    {
        List<ArticleLikeDisslikeDto> GetUsersLikedOrDisslikedArticle(long articleId, LikeDisslikeType likeDisslikeType);
    }
}