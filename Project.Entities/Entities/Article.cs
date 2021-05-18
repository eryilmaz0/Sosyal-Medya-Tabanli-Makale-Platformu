using System.Collections.Generic;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class Article : EntityBase<long>, ISoftDeletable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ArticleFile { get; set; }
        public long UserId { get; set; }
        public long ArticleCategoryId { get; set; }
        public string Picture { get; set; }
        public long ViewCount { get; set; }


        //NAVIGATION PROPS
        public virtual User User { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public virtual List<ArticleComment> ArticleComments { get; set; }
        public virtual List<ArticleLikeDisslike> ArticleLikeDisslikes { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }


        public Article() : base()
        {
            this.Picture = "DefaultArticleCoverPhoto.jpg";
            this.Favorites = new List<Favorite>();
            this.ArticleComments = new List<ArticleComment>();
            this.ArticleLikeDisslikes = new List<ArticleLikeDisslike>();
        }
        
            
        
    }
}
