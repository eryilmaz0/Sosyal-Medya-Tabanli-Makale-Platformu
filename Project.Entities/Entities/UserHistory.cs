using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Entities
{
    public class UserHistory : EntityBase<long>, ISoftDeletable
    {
        public long UserId { get; set; }
        public long ArticleCategoryId { get; set; }
       


        //NAVIGATION PROPS
        public virtual User User { get; set; }
        public virtual ArticleCategory ArticleCategory { get; set; }
        




        public UserHistory():base(){}
        

    }
}