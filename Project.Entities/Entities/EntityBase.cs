using System;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class EntityBase<T> : IEntity<T>
    {
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }


        public EntityBase()
        {
            this.IsDeleted = false;
            this.Created = DateTime.Now;
        }
    }
}