using System.Text.Json.Serialization;

namespace ChatDomain;

public class UserGroup
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public virtual User User { get; set; }
    public virtual Group Group { get; set; }
}

