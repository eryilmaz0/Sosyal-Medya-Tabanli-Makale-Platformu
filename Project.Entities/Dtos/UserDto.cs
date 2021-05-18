using System;
using System.Collections.Generic;
using Project.Core.Entities;
using Project.Entities.Entities;
using Project.Entities.Enums;

namespace Project.Entities.Dtos
{
    public class UserDto : DtoBase
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string About { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime Created { get; set; }
        public bool? isDeleted { get; set; }


        //NAV PROPS
        public List<ArticleDto> Articles { get; set; }
        public List<AuthorizationAppealDto> AuthorizationAppeals { get; set; }
        public List<UserHistoryDto> UserHistories { get; set; }
        public List<ArticleCommentDto> ArticleComments { get; set; }
        public List<ArticleLikeDisslikeDto> ArticleLikeDisslikes { get; set; }
        public List<TopicDto> Topics { get; set; }
        public List<TopicCommentDto> TopicComments { get; set; }
        public List<ChatDto> Chats { get; set; }
        public List<ChatDto> Chats2 { get; set; }
        public List<ChatCommentDto> ChatComments { get; set; }
        public List<FavoriteDto> Favorites { get; set; }
  }
}
