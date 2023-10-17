using Corvus.Nest.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BlogPostBase : ComponentBase
{
    [Parameter]
    public int PostID { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;

    public BlogModel? PreviousPost { get; set; }

    public BlogModel? NextPost { get; set; }

    protected string PostHtml { get; set; } = string.Empty;

    private async Task<List<BlogModel>> GetBlogs()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "datas", $"blogs.json");

        var res = await HttpClient.GetAsync(requestUri);
        var json = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<List<BlogModel>>(json) ?? new();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JSRuntime.InvokeVoidAsync("Prism.highlightAll");
    }

    protected override async Task OnParametersSetAsync()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "posts", $"post{PostID}.html");

        var res = await HttpClient.GetAsync(requestUri);
        PostHtml = await res.Content.ReadAsStringAsync();

        var blogs = await GetBlogs();

        PreviousPost = blogs.SingleOrDefault(x => x.Id.Equals(PostID - 1));
        NextPost = blogs.SingleOrDefault(x => x.Id.Equals(PostID + 1));

        await base.OnParametersSetAsync();
    }
}