using System;
using Microsoft.AspNetCore.Identity;
using Project.Core.Entities;

namespace Project.Entities.Entities
{
    public class Role : IdentityRole<long>, IEntity<long>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }



        public Role()
        {
            this.IsDeleted = false;
            this.Created = DateTime.Now;
        }
    }
}