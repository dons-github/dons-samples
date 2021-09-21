using FluentValidation;
using SampleContact.Data.Models;

namespace SampleContact.Data.Services.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(c => c.Email)
                .NotNull()
                .WithMessage("Email is required!");

            RuleFor(c => c.Name != null ? c.Name.First : "")
                .Length(0, 50);

            RuleFor(c => c.Name != null ? c.Name.Last : "")
                .Length(0, 50);

        }
    }
}
