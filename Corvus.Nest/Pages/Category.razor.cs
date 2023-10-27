using Corvus.Nest.Models;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class CategoryBase : BaseComponent
{
    public List<CategoryModel>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categories = await GetJsonString<List<CategoryModel>>($"category.json");

        var blogs = await GetJsonString<List<BlogModel>>($"blogs.json");

        Categories?.ForEach(
            x =>
            {
                x.PostCount = blogs?.Where(y => (y.Category?.Equals(x.Id) ?? false)).Count() ?? 0;
            });
    }
}