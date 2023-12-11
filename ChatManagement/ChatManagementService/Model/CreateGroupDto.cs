using FluentValidation;

namespace ChatManagementService.Model
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public List<Guid> UsersId { get; set; }
    }

    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.UsersId).NotEmpty();
        }
    }
}
