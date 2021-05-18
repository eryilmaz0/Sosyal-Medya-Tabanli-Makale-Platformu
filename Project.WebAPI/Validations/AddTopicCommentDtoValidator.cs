using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class AddTopicCommentDtoValidator : AbstractValidator<AddTopicCommentDto>
    {
        public AddTopicCommentDtoValidator()
        {
            RuleFor(x => x.TopicId).NotNull().WithMessage("Topic ID Boş Olamaz.").GreaterThan(0)
                .WithMessage("Lütfen Topic ID Giriniz.");

            RuleFor(x => x.Comment).NotNull().WithMessage("İçerik Boş Olamaz.").MinimumLength(1)
                .WithMessage("Lütfen İçerik Giriniz.");
        }
    }
}