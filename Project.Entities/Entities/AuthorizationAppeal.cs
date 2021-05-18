using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Entities
{
    public class AuthorizationAppeal : EntityBase<long>, ISoftDeletable
    {
        public string AppealDescription { get; set; }
        public long UserId { get; set; }
        public AppealStatus AppealStatus { get; set; }

        //NAVIGATION PROPS
        public virtual User User { get; set; }


        public AuthorizationAppeal() : base()
        {
            this.AppealStatus = AppealStatus.Waiting;
        }
        
            
        
    }
}