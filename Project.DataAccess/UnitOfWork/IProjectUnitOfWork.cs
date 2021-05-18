
using Project.DataAccess.Abstract;

namespace Project.DataAccess.UnitOfWork
{
    public interface IProjectUnitOfWork : IUnitOfWork
    {
        IArticleCommentRepository ArticleComments { get; set; }
        IArticleLikeDisslikeRepository ArticleLikeDisslikes { get; set; }
        IArticleRepository Articles { get; set; }
        IArticleCategoryRepository ArticleCategories { get; set; }
        IAuthorizationAppealRepository AuthorizationAppeals { get; set; }
        IChatCommentRepository ChatComments { get; set; }
        IChatRepository Chats { get; set; }
        IFavoriteRepository Favorites { get; set; }
        ITopicCommentRepository TopicComments { get; set; }
        ITopicRepository Topics { get; set; }
        IUserHistoryRepository UserHistories { get; set; }
        IUserRepository Users { get; set; }
    }
}