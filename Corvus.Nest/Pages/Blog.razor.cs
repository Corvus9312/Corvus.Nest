using Corvus.Nest.Models;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BlogBase : BaseComponent
{
    public List<BlogModel>? Blogs { get; set; }

    [Parameter] public string? Category { get; set; } = null;

    public string? CategoryTitle { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Blogs = await GetJsonString<List<BlogModel>>($"blogs.json") ?? new();

        Blogs = Category is null ? Blogs : Blogs.Where(x => x.Category?.Equals(Category) ?? false).ToList();

        Blogs.ForEach(
            x =>
            {
                x.Url = Category is null ? $"/blog/{x.Id}" : $"/blog/{Uri.EscapeDataString(Category)}/{x.Id}";
            });


        var categoryTitle = await GetJsonString<List<CategoryModel>>($"category.json");

        CategoryTitle = categoryTitle?.SingleOrDefault(x => x.Id.Equals(Category ?? string.Empty))?.Title;
    }


}
