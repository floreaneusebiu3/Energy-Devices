using FluentValidation;

namespace ChatManagementService.Model;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Role).NotEmpty();
    }
}