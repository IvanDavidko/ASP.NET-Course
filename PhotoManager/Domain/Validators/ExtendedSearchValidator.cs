
using Domain.Models;
using FluentValidation;

namespace Domain.Validators
{
    public class ExtendedSearchValidator : AbstractValidator<ExtendedSearch>
    {
        public ExtendedSearchValidator()
        {
            RuleFor(request => request.Name)
                .Length(0, 64)
                .WithMessage("Name is not valid");

            RuleFor(request => request.Description)
                .Length(0, 64)
                .WithMessage("Description is not valid");

            RuleFor(request => request.PlaceCreated)
                .Length(0, 64)
                .WithMessage("Place created is not valid");

            RuleFor(request => request.CameraModel)
                .Length(0, 64)
                .WithMessage("Camera model is not valid");

            RuleFor(request => request.FocalLengthOfTheLens)
                .Length(0, 64)
                .WithMessage("Focal length of the lens is not valid");

            RuleFor(request => request.Diaphragm)
                .Length(0, 64)
                .WithMessage("Diaphragm is not valid");

            RuleFor(request => request.ShutterSpeed)
                .Length(0, 64)
                .WithMessage("Shutter speed is not valid");

            RuleFor(request => request.ISO)
                .Length(0, 64)
                .WithMessage("ISO is not valid");
        }
    }
}
