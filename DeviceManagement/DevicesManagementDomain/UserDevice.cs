namespace DevicesManagementDomain
{
    public class UserDevice
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }    
        public string Address { get; set; } 
        public Device Device { get; set; }
        public User User { get; set; }
    }
}
