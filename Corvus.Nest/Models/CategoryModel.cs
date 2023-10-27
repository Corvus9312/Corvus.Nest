namespace Corvus.Nest.Models;

public class CategoryModel
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int PostCount { get; set; } = 0;
}
