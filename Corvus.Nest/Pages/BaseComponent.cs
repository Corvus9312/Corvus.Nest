using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Corvus.Nest.Pages;

public class BaseComponent : ComponentBase
{
    [Inject] public NavigationManager Navigator { get; set; } = null!;

    [Inject] public HttpClient HttpClient { get; set; } = null!;

    [Inject] public IJSRuntime JSRuntime { get; set; } = null!;

    public async Task<T?> GetJsonString<T>(string fileName) where T : class
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "datas", fileName);

        var res = await HttpClient.GetAsync(requestUri);
        var json = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(json) ?? null;
    }
}
