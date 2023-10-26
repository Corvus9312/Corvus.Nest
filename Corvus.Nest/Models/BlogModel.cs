namespace Corvus.Nest.Models;

public class BlogModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Url { get; set; } = string.Empty;

    public string Description { get; set; } = null!;

    public string? Category { get; set; } = null;

    public DateTime CreateTime { get; set; }
}