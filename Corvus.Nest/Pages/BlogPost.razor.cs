﻿using Microsoft.AspNetCore.Components;

namespace Corvus.Nest.Pages;

public class BlogPostBase : ComponentBase
{
    [Parameter]
    public int PostID { get; set; }

    [Inject]
    public NavigationManager Navigator { get; set; } = null!;

    [Inject]
    public HttpClient HttpClient { get; set; } = null!;

    protected string PostHtml { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var requestUri = Path.Combine(Navigator.BaseUri, "posts", $"post{PostID}.html");

        var res = await HttpClient.GetAsync(requestUri);
        PostHtml = await res.Content.ReadAsStringAsync();
    }
}