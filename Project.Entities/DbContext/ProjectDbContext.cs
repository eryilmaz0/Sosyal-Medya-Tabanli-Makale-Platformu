using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Entities.Entities;
using Project.Entities.EntityConfigurations;

namespace Project.Entities.DbContext
{
    public class ProjectDbContext : EfDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\Eren;Database=ProjectDatabase2;Trusted_Connection=True;MultipleActiveResultSets=True");
        }


        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<AuthorizationAppeal> AuthorizationAppeals { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<ArticleLikeDisslike> ArticleLikeDisslikes { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicComment> TopicComments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatComment> ChatComments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ArticleCommentConfiguration());
            builder.ApplyConfiguration(new ArticleConfiguration());
            builder.ApplyConfiguration(new ArticleLikeDisslikeConfiguration());
            builder.ApplyConfiguration(new AuthorizationAppealConfiguration());
            builder.ApplyConfiguration(new ChatCommentsConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
            builder.ApplyConfiguration(new FavoriteConfiguration());
            builder.ApplyConfiguration(new TopicCommentsConfiguration());
            builder.ApplyConfiguration(new TopicConfiguration());
            builder.ApplyConfiguration(new UserHistoryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ArticleCategoryConfiguration());

            base.OnModelCreating(builder);
        }
    }
}