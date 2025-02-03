namespace Orla.Core.Responses.Details;

public class YouTubeContentDetailsResult
{
    public Item[] items { get; set; }
}

public class Item
{   
    public ContentDetails? contentDetails { get; set; }
}

public class ContentDetails
{
    public string duration { get; set; }
}