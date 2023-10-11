using Corvus.Nest.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BlogBase : ComponentBase
{
    public List<BlogModel>? Blogs { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "datas", $"blogs.json");

        var res = await HttpClient.GetAsync(requestUri);
        var json = await res.Content.ReadAsStringAsync();

        Blogs = JsonSerializer.Deserialize<List<BlogModel>>(json);
    }
}
