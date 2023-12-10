namespace ChatDomain;
public class Message
{
    public Guid Id { get; set; }
    public string MessageText { get; set; }
    public bool Seen { get; set; }
    public long Timestamp { get; set; }
    public Guid SenderUserId { get; set; }
    public Guid? DestionationUserId { get; set; }
    public Guid? GroupId { get; set; }
    public virtual User SenderUser { get; set; }
    public virtual User? DestionationUser { get; set; }
    public virtual Group? Group { get; set; }
}
