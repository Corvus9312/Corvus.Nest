using Corvus.Nest.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BlogBase : BaseComponent
{
    public List<BlogModel>? Blogs { get; set; }

    [Parameter]public string? Category { get; set; } = null;

    protected override async Task OnParametersSetAsync()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "datas", $"blogs.json");

        var res = await HttpClient.GetAsync(requestUri);
        var json = await res.Content.ReadAsStringAsync();

        Blogs = JsonSerializer.Deserialize<List<BlogModel>>(json) ?? new();

        Blogs = Category is null ? Blogs : Blogs.Where(x => x.Category?.Equals(Category) ?? false).ToList();

        Blogs.ForEach(
            x =>
            {
                x.Url = Category is null ? $"/blog/{x.Id}" : $"/blog/{Category}/{x.Id}";
            });
    }
}
