
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class PhotoValidator : AbstractValidator<Photo>
    {
        public PhotoValidator()
        {
            RuleFor(request => request.Image)
                .NotNull()
                .WithMessage("Image must be valid");

            RuleFor(request => request.ImageSize)
                .NotNull()
                .WithMessage("Image size must be valid");

            RuleFor(request => request.ImageType)
                .NotNull()
                .WithMessage("Image type must be valid");

            RuleFor(request => request.UserId)
                .GreaterThan(0)
                .WithMessage("You should login");
        }
    }
}
