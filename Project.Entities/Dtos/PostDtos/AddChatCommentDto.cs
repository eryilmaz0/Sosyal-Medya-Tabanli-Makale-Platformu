using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Entities.Dtos.PostDtos
{
  public class AddChatCommentDto : DtoBase
  {
    public long ReceiverId { get; set; }
    public string Content { get; set; }
  }
}
