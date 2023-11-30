using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BaseComponent : ComponentBase
{
    [Inject] public NavigationManager Navigator { get; set; } = null!;

    [Inject] public IJSRuntime JSRuntime { get; set; } = null!;

    [Inject] public IWebHostEnvironment Environment { get; set; } = null!;

    public T? GetJsonString<T>(string fileName) where T : class
    {        
        var folderPath = Path.Combine(Environment.ContentRootPath, "Datas");

        var json = File.ReadAllText(Path.Combine(folderPath, fileName));

        return JsonSerializer.Deserialize<T>(json) ?? null;
    }

    public string GetPostHtml(int postID)
    {
        var folderPath = Path.Combine(Environment.ContentRootPath, "Posts");

        return File.ReadAllText(Path.Combine(folderPath, $"post{postID}.html"));
    }
}