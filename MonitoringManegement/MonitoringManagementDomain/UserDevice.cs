namespace MonitoringManagementDomain
{
    public class UserDevice
    {
        public Guid Id { get; set; }
        public long MaximuHourlyEnergyConsumption { get; set; }
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
        public virtual List<Measurement> Measurement { get; set; }

        public UserDevice()
        { 
            Measurement = new List<Measurement>();
        }
    }
}
