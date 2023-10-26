using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Corvus.Nest.Pages;

public class BaseComponent : ComponentBase
{
    [Inject] public NavigationManager Navigator { get; set; } = null!;

    [Inject] public HttpClient HttpClient { get; set; } = null!;

    [Inject] public IJSRuntime JSRuntime { get; set; } = null!;
}
