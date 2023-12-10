namespace DevicesManagementDomain
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long MaximuHourlyEnergyConsumption { get; set; }  
        public List<UserDevice> UserDevices { get; set; }
    }
}