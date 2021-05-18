using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class Chat : EntityBase<long>, ISoftDeletable
    {

        [ForeignKey(nameof(FirstUser)), Column(Order = 0)]
        public long FirstUserId { get; set; }

        [ForeignKey(nameof(SecondUser)), Column(Order = 0)]
        public long SecondUserId { get; set; }


        //NAVIGATION PROPS
        public virtual User FirstUser { get; set; }
        public virtual User SecondUser { get; set; }
        public virtual List<ChatComment> ChatComments { get; set; }


        public Chat() : base()
        {
            this.ChatComments = new List<ChatComment>();
        }
        
            
        
    }
}