using BlazorWasmPlanner.Shared.Models;
using FluentValidation;

namespace BlazorWasmPlanner.Shared.Validators
{
    public class PlanValidator : AbstractValidator<PlanDetail>
    {
        public PlanValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(80)
                .WithMessage("Title must be less than 80 characters");

            RuleFor(p => p.Description)
                 .MaximumLength(500)
                 .WithMessage("Description must be less than 500 characters");

        }
    }

}
