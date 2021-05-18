using System.Collections.Generic;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class ArticleCategory:EntityBase<long>, ISoftDeletable
    {
        public string Category { get; set; }
        public string CategoryDescription { get; set; }


        //NAVIGATION PROPS
        public virtual List<Article> Articles { get; set; }
        public virtual List<UserHistory> UserHistories { get; set; }


        public ArticleCategory():base()
        {
            
        }

    }
}