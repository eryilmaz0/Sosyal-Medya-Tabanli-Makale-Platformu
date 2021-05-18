using Autofac;
using Microsoft.AspNetCore.Http;
using Project.Business.Abstract;
using Project.Business.Concrete;
using Project.DataAccess.Abstract;
using Project.DataAccess.EntityFrameworkWithSoftDelete;
using Project.DataAccess.UnitOfWork;
using Project.Entities.DbContext;

namespace Project.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //IOC

            //REPOSITORIES
            builder.RegisterType<EfUArticleCommentRepository>().As<IArticleCommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUArticleLikeDisslikeRepository>().As<IArticleLikeDisslikeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUArticleCategoryRepository>().As<IArticleCategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUAuthorizationAppealRepository>().As<IAuthorizationAppealRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUChatCommentRepository>().As<IChatCommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUChatRepository>().As<IChatRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUFavoriteRepository>().As<IFavoriteRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUTopicCommentRepository>().As<ITopicCommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUTopicRepository>().As<ITopicRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUUserHistoryRepository>().As<IUserHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EfUUserRepository>().As<IUserRepository>().InstancePerLifetimeScope();




            //SERVICES
            builder.RegisterType<ArticleLikeDisslikeManager>().As<IArticleLikeDisslikeService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticleManager>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<ArticleCategoryManager>().As<IArticleCategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorizationAppealManager>().As<IAuthorizationAppealService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<ChatManager>().As<IChatService>().InstancePerLifetimeScope();
            builder.RegisterType<DocumentManager>().As<IDocumentService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailManager>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<FavoriteManager>().As<IFavoriteService>().InstancePerLifetimeScope();
            builder.RegisterType<TopicManager>().As<ITopicService>().InstancePerLifetimeScope();
            builder.RegisterType<UserHistoryManager>().As<IUserHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();


            builder.RegisterType<ProjectDbContext>().As<ProjectDbContext>().InstancePerLifetimeScope();

            //UOW
            builder.RegisterType<ProjectUnitOfWork>().As<IProjectUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();



        }
    }
}