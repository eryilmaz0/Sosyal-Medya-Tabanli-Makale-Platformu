using System.Collections.Generic;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class TopicComment : EntityBase<long>, ISoftDeletable
    {
        public string Comment { get; set; }
        public long TopicId { get; set; }
        public long UserId { get; set; }


        //NAVIGATION PROPS
        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }


        public TopicComment() : base()
        {
            
        }
        
            
        
    }
}