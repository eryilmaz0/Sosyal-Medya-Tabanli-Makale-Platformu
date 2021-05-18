using FluentValidation;
using Project.Entities.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebAPI.Validations
{
  public class AddChatCommentDtoValidator : AbstractValidator<AddChatCommentDto>
  {
    public AddChatCommentDtoValidator()
    {
      RuleFor(x => x.ReceiverId).NotNull().WithMessage("Alıcı ID Boş Olamaz.");
      RuleFor(x => x.ReceiverId).GreaterThan(0).WithMessage("Lütfen Alıcı ID Giriniz.");

      RuleFor(x => x.Content).NotNull().WithMessage("Mesaj Boş Olamaz.");
      RuleFor(x => x.Content).MinimumLength(1).WithMessage("Lütfen Mesaj İçeriği Giriniz.");
    }
  }
}
