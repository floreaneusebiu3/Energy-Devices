using FluentValidation;

namespace DeviceManagementService.Mappers.Dtos
{
    public class DeviceDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long MaximuHourlyEnergyConsumption { get; set; }
    }

    public class DeviceDtoValidator : AbstractValidator<DeviceDto>
    {
        public DeviceDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.MaximuHourlyEnergyConsumption).NotNull().GreaterThan(0);
        }
    }
}
