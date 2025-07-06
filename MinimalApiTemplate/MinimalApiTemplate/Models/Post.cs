namespace MinimalApiTemplate.Models;

public class Post
{
    public int Id { get; set; }
    public DateTime TimeOfCreation { get; set; }
    public string? TextLayout { get; set; }
    public string? AuthorUsername { get; set; }
}