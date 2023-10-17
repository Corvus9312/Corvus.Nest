using Corvus.Nest.Models;
using Microsoft.AspNetCore.Components;

namespace Corvus.Nest.Pages;

public class BlogPostFooterBase : ComponentBase
{
    [Parameter]
    public BlogModel? PreviousPost { get; set; }

    [Parameter]
    public BlogModel? NextPost { get; set; }
}