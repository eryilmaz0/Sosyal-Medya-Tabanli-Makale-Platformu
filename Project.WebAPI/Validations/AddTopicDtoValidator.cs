using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddTopicDtoValidator : AbstractValidator<AddTopicDto>
    {
        public AddTopicDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().WithMessage("Başlık Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen Başlık Giriniz.");


            RuleFor(x => x.Title).MaximumLength(150).WithMessage("Başlık 150 Karakteri Geçemez.");


            RuleFor(x => x.Content).NotNull().WithMessage("İçerik Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen İçerik Giriniz.");

            
        }
    }
}