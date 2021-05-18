using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class ChatComment : EntityBase<long>, ISoftDeletable
    {
        public string Comment { get; set; }
        public long ChatId { get; set; }
        public long UserId { get; set; }


        //NAVIGATION PROPERTY
        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}