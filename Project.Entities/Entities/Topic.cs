 using System.Collections.Generic;
 using Project.Core.Entities;

 namespace Project.Entities.Entities
{
    public class Topic : EntityBase<long>, ISoftDeletable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }


        //NAVIGATION PROPS
        public virtual User User { get; set; }
        public virtual List<TopicComment> TopicComments { get; set; }
        


        public Topic() : base()
        {
            this.TopicComments = new List<TopicComment>();
        }
        
            
        
    }
}