using Corvus.Nest.Models;
using Corvus.Nest.Pages;

namespace Corvus.Nest.Components.Pages;

public class CategoryBase : BaseComponent
{
    public List<CategoryModel>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categories = GetJsonString<List<CategoryModel>>($"category.json");

        var blogs = GetJsonString<List<BlogModel>>($"blogs.json");

        Categories?.ForEach(
            x =>
            {
                x.PostCount = blogs?.Where(y => (y.Category?.Equals(x.Id) ?? false)).Count() ?? 0;
            });

        await base.OnInitializedAsync();
    }
}