namespace ChatManagementService.Model;

public class MessageDto
{
    public string MessageText { get; set; }
    public Guid SenderId { get; set; }
    public Guid DestionationUserId { get; set; }
    public string Status { get; set; }  
    public string SenderName { get; set;}
}   