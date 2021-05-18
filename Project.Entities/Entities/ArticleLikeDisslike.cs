using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Entities
{
    public class ArticleLikeDisslike : EntityBase<long>, ISoftDeletable
    {
        public LikeDisslikeType LikeDisslikeType { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }


        //NAVIGATION PROPS
        public virtual User User { get; set; }
        public virtual Article Article { get; set; }


        public ArticleLikeDisslike():base(){}
        
            
        
    }
}