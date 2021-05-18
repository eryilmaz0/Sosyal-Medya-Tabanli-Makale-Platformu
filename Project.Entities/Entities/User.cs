using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Project.Core.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Entities
{
    public class User : IdentityUser<long>, IEntity<long>, ISoftDeletable
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string About { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }


        //NAVIGATION PROPS
        public virtual List<Article> Articles { get; set; }
        public virtual List<AuthorizationAppeal> AuthorizationAppeals { get; set; }
        public virtual List<UserHistory> UserHistories { get; set; }
        public virtual List<ArticleComment> ArticleComments { get; set; }
        public virtual List<ArticleLikeDisslike> ArticleLikeDisslikes { get; set; }
        public virtual List<Topic> Topics { get; set; }
        public virtual List<TopicComment> TopicComments { get; set; }
        public virtual List<ChatComment> ChatComments { get; set; }
        public virtual List<Favorite> Favorites { get; set; }

        [InverseProperty(nameof(Chat.FirstUser))]
        public virtual List<Chat> Chats { get; set; }
        [InverseProperty(nameof(Chat.SecondUser))]
        public virtual List<Chat> Chats2 { get; set; }


        public User()
        {
            this.IsDeleted = false;
            this.Created = DateTime.Now;
            this.Gender = Gender.Unspecified;
            this.Picture = "defaultuserpicture.png";

            this.Articles = new List<Article>();
            this.AuthorizationAppeals = new List<AuthorizationAppeal>();
            this.UserHistories = new List<UserHistory>();
            this.ArticleComments = new List<ArticleComment>();
            this.ArticleLikeDisslikes = new List<ArticleLikeDisslike>();
            this.Topics = new List<Topic>();
            this.TopicComments = new List<TopicComment>();
            this.Chats = new List<Chat>();
            this.ChatComments = new List<ChatComment>();
            this.Chats2 = new List<Chat>();
            this.Favorites = new List<Favorite>();
            
        }

        
    }
}
