using Corvus.Nest.Models;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class CategoryBase : BaseComponent
{
    public List<CategoryModel>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "datas", $"blogs.json");

        var res = await HttpClient.GetAsync(requestUri);
        var json = await res.Content.ReadAsStringAsync();

        var blogs = JsonSerializer.Deserialize<List<BlogModel>>(json);

        Categories = blogs?
            .Where(x => x.Category is not null)
            .GroupBy(x => x.Category)
            .Select(x => new CategoryModel { Category = x.Key ?? string.Empty, PostCount = x.Count() })
            .ToList();
    }
}