namespace MonitoringManagementDomain
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public long TimeStamp { get; set; }
        public double Consumption { get; set; }
        public Guid UserDeviceId { get; set; }
        public virtual UserDevice UserDevice { get; set; }
    }
}