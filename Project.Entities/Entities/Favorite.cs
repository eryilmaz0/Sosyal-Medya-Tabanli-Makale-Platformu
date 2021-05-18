using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class Favorite : EntityBase<long>, ISoftDeletable
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }


        //NAVIGATION PROPS
        public virtual User User { get; set; }
        public virtual Article Article { get; set; }


        public Favorite():base(){}
        
            
        
    }
}