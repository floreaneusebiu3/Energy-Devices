using FluentValidation;

namespace DeviceManagementService.Mappers.UserDeviceDeviceMappers.Dtos
{
    public class UserDevicePostDto
    {
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public string Address { get; set; }
    }

    public class UserDevicePostDtoValidator : AbstractValidator<UserDevicePostDto>
    {
        public UserDevicePostDtoValidator()
        {
            RuleFor(x => x.Address).NotEmpty()
                .WithMessage("Address is mandatory");
        }
    }
}
