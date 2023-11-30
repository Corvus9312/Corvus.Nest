using Corvus.Nest.Models;
using Corvus.Nest.Pages;
using Microsoft.AspNetCore.Components;

namespace Corvus.Nest.Components.Pages;

public class BlogBase : BaseComponent
{
    public List<BlogModel>? Blogs { get; set; }

    [Parameter] public string? Category { get; set; } = null;

    public string? CategoryTitle { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        Blogs = GetJsonString<List<BlogModel>>($"blogs.json") ?? new();

        Blogs = Category is null ? Blogs : Blogs.Where(x => x.Category?.Equals(Category) ?? false).ToList();

        Blogs.ForEach(
            x =>
            {
                x.Url = Category is null ? $"/blog/{x.Id}" : $"/blog/{Uri.EscapeDataString(Category)}/{x.Id}";
            });


        var categoryTitle = GetJsonString<List<CategoryModel>>($"category.json");

        CategoryTitle = categoryTitle?.SingleOrDefault(x => x.Id.Equals(Category ?? string.Empty))?.Title;
    }
}