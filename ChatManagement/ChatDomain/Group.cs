using System.Text.Json.Serialization;

namespace ChatDomain;

public class Group
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AdminId { get; set; }
    public virtual User Admin { get; set; }
    public virtual List<Message> Messages { get; set; }
    public virtual List<UserGroup> UserGroups { get; set; }

    public Group()
    {
        Messages = new List<Message>();
        UserGroups = new List<UserGroup>();
    }
}

