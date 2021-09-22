using FluentValidation;
using SampleContact.Data.Models;

namespace SampleContact.Data.Services.Validators
{
    /// <summary>
    /// This class contains the validation for a <see cref="Contact"/>
    /// </summary>
    public class ContactValidator : AbstractValidator<Contact>
    {
        /// <summary>
        /// The validation rules are defined in this constructor
        /// </summary>
        public ContactValidator()
        {
            // Normally, I would contact the business / business anaylist for specific rules
            // In this case, I created a few just for demonstration purposes.
            // TODO - Document these rules

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
