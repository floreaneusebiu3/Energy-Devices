using System.Text.Json.Serialization;

namespace ChatDomain;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public virtual List<Message> ReceivedMessages { get; set; }
    public virtual List<Message> SentMessages { get; set; }
    public virtual List<Group> GroupsAsAdmin { get; set; }
    public virtual List<UserGroup> UserGroups { get; set; }

    public User()
    {
        ReceivedMessages = new List<Message>();
        SentMessages = new List<Message>();
        GroupsAsAdmin = new List<Group>();
        UserGroups = new List<UserGroup>();
    }
}
