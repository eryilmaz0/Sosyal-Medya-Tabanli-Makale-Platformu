using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateTopicCommentDtoValidator : AbstractValidator<UpdateTopicCommentDto>
    {
        public UpdateTopicCommentDtoValidator()
        {
            RuleFor(x => x.TopicCommentId).NotNull().WithMessage("Yorum ID Boş Olamaz.").GreaterThan(0)
                .WithMessage("Yorum ID giriniz.");

            RuleFor(x => x.Comment).NotNull().WithMessage("İçerik Boş Olamaz.").MinimumLength(1)
                .WithMessage("İçerik Giriniz.");
        }
    }
}