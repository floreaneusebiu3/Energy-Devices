using FluentValidation;

namespace ChatManagementService.Model
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public Guid AdminId { get; set; }
        public List<Guid> UsersId { get; set; }
    }

    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupDtoValidator()
        {
            RuleFor(x => x.AdminId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
