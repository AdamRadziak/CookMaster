using CookMaster.Aplication.DTOs;
using FluentValidation;

namespace CookMaster.Aplication.Validation
{
    public class AddUpdateUserDTOValidator : AbstractValidator<AddUpdateUserDTO>
    {
        public AddUpdateUserDTOValidator()
        {
            RuleFor(request => request.Email).EmailAddress();
            RuleFor(request => request.Password)
                //.NotEmpty()
                .MinimumLength(6).WithMessage("Minimalna długość pola 'Hasło' to {MinLength} znaków")
                .Matches("[A-Z]").WithMessage("'Hasło' musi zawierać co najmniej jedną wielką literę.")
                .Matches("[a-z]").WithMessage("'Hasło' musi zawierać co najmniej jedną małą literę.")
                .Matches(@"\d").WithMessage("'Hasło' musi zawierać co najmniej jedną cyfrę.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'Hasło' musi zawierać co najmniej jeden znak specjalny.")
                .Matches("^[^£# “”]*$").WithMessage("'Hasło' nie może zawierać znaków £ # “” ani spacji.")
                .When(request => request.IsPassswordUpdate).When(request => request.IsEmailUpdate);
            // .Must(pass => !blacklistedWords.Any(word => pass.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
            //     .WithMessage("'Hasło' zawiera niedozwoloną frazę.");
        }
    }
}
