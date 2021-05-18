using FluentValidation;
using Project.Entities.Dtos.PostDtos;

namespace Project.WebAPI.Validations
{
    public class UpdateTopicDtoValidator : AbstractValidator<UpdateTopicDto>
    {
        public UpdateTopicDtoValidator()
        {
            RuleFor(x => x.TopicId).NotNull();
            RuleFor(x => x.TopicId).GreaterThan(0);
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Content).NotNull();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}