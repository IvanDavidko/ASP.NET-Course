
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class AlbumValidator : AbstractValidator<Album>
    {
        public AlbumValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty()
                .NotNull()
                .Length(1, 256)
                .WithMessage("The Title must be valid");

            RuleFor(request => request.Description)
                .NotNull()
                .Length(1, 2024)
                .WithMessage("The Description must be valid");

            RuleFor(request => request.UserId)
                .GreaterThan(0)
                .WithMessage("You should login");
        }
    }
}
