namespace Orla.Core.Models;

public class Message
{
    public List<string> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public Message(IEnumerable<string> to, string subject, string content)
    {
        To = new List<string>();
        To.AddRange(to.Select(x => x));
        Subject = subject;
        Content = content;
    }
}
