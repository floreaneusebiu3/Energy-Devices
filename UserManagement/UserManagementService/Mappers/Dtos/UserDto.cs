using FluentValidation;

namespace UserManagement.Controllers.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string Role { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Role).NotEmpty().Matches("(?i)^(client|admin)$");
        }
    }
}
