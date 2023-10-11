namespace Corvus.Nest.Models;

public class BlogModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Url
    {
        get
        {
            return $"/Corvus.Nest/blog/{Id}";
        }
    }

    public string Description { get; set; } = null!;

    public DateTime CreateTime { get; set; }
}
