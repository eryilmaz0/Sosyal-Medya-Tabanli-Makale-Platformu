using Project.DataAccess.Abstract;
using Project.Entities.DbContext;

namespace Project.DataAccess.UnitOfWork
{
    public class ProjectUnitOfWork : EfUnitOfWork, IProjectUnitOfWork
    {


        //ALL THE REPOSITORIES
        public IArticleCommentRepository ArticleComments { get; set; }
        public IArticleLikeDisslikeRepository ArticleLikeDisslikes { get; set; }
        public IArticleRepository Articles { get; set; }
        public IArticleCategoryRepository ArticleCategories { get; set; }
        public IAuthorizationAppealRepository AuthorizationAppeals { get; set; }
        public IChatCommentRepository ChatComments { get; set; }
        public IChatRepository Chats { get; set; }
        public IFavoriteRepository Favorites { get; set; }
        public ITopicCommentRepository TopicComments { get; set; }
        public ITopicRepository Topics { get; set; }
        public IUserHistoryRepository UserHistories { get; set; }
        public IUserRepository Users { get; set; }
       



        //DI
        public ProjectUnitOfWork
        (
                ProjectDbContext _context,
                IArticleCommentRepository articleComments,
                IArticleLikeDisslikeRepository articleLikeDisslikes,
                IArticleRepository articles,
                IArticleCategoryRepository articleCategories,
                IAuthorizationAppealRepository authorizationAppeals,
                IChatCommentRepository chatComments,
                IChatRepository chats,
                IFavoriteRepository favorites,
                ITopicCommentRepository topicComments,
                ITopicRepository topics,
                IUserHistoryRepository userHistories,
                IUserRepository users

        ):base(_context)

        {
            ArticleComments = articleComments;
            ArticleLikeDisslikes = articleLikeDisslikes;
            Articles = articles;
            ArticleCategories = articleCategories;
            AuthorizationAppeals = authorizationAppeals;
            ChatComments = chatComments;
            Chats = chats;
            Favorites = favorites;
            TopicComments = topicComments;
            Topics = topics;
            UserHistories = userHistories;
            Users = users;
        }

    }
}