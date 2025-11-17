using FluentValidation;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.FirstNameIsRequired);
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Email).NotEmpty().WithErrorCode(UserErrorCodes.CreateUser.MissingEmail)
            .EmailAddress().WithErrorCode(UserErrorCodes.CreateUser.InvalidEmail);
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
    }
}
