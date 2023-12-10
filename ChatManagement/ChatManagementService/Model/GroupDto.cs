namespace ChatManagementService.Model
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AdminId { get; set; }
        public List<string> Members { get; set; }

        public GroupDto()
        {
            Members = new List<string>();
        }
    }
}
