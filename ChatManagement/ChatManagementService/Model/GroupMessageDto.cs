using FluentValidation;

namespace ChatManagementService.Model;

public class GroupMessageDto
{
    public string MessageText { get; set; }
    public Guid GroupId { get; set; }
}

public class GroupMessageDtoValidator : AbstractValidator<GroupMessageDto>
{
    public GroupMessageDtoValidator()
    {
        RuleFor(x => x.MessageText).NotEmpty();
        RuleFor(x => x.GroupId).NotEmpty();
    }
}