using FluentValidation;

namespace ChatManagementService.Model;

public class UserMessageDto
{
    public string MessageText { get; set; }
    public Guid DestionationUserId { get; set; }
}

public class UserMessageDtoValidator : AbstractValidator<UserMessageDto>
{
    public UserMessageDtoValidator()
    {
        RuleFor(x => x.MessageText).NotEmpty();
        RuleFor(x => x.DestionationUserId).NotEmpty();
    }
}
