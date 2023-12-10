using FluentValidation;

namespace UserManagement.Controllers.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }   
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
