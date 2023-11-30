using Corvus.Nest.Models;
using Corvus.Nest.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Corvus.Nest.Components.Pages;

public class BlogPostBase : BaseComponent
{
    [Parameter] public int PostID { get; set; }

    [Parameter] public string? Category { get; set; } = null;

    public BlogModel? PreviousPost { get; set; }

    public BlogModel? NextPost { get; set; }

    protected string PostHtml { get; set; } = string.Empty;

    protected string PostTitle { get; set; } = string.Empty;

    private List<BlogModel> GetBlogs()
    {
        var blogs = GetJsonString<List<BlogModel>>("blogs.json") ?? [];

        blogs = Category is null ? blogs : blogs.Where(x => x.Category?.Equals(Category) ?? false).ToList(); ;

        blogs.ForEach(
            x =>
            {
                x.Url = Category is null ? $"/blog/{x.Id}" : $"/blog/{Uri.EscapeDataString(Category)}/{x.Id}";
            });

        return blogs;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JSRuntime.InvokeVoidAsync("Prism.highlightAll");
    }

    protected override async Task OnParametersSetAsync()
    {
        PostHtml = GetPostHtml(PostID);

        var blogs = GetBlogs();

        PostTitle = blogs.SingleOrDefault(x => x.Id.Equals(PostID))?.Title ?? string.Empty;
        PreviousPost = blogs.SingleOrDefault(x => x.Id.Equals(PostID - 1));
        NextPost = blogs.SingleOrDefault(x => x.Id.Equals(PostID + 1));

        await base.OnParametersSetAsync();
    }
}