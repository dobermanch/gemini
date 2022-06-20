using Gemini.Portal.Client.Components.Svg.Shapes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Gemini.Portal.Client.Components.Svg;

public partial class Svg
{
    private IJSObjectReference? _jSModule;

    [Inject]
    protected IJSRuntime JSRuntime { get; set; } = null!;

    public Box BBox { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _jSModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Components/Svg/Svg.razor.js");
        BBox = await GetBoundingBox(SVGElementReference);
    }

    public async ValueTask Focus(ElementReference elementReference)
    {
        await _jSModule!.InvokeVoidAsync("focus", elementReference);
    }

    public async ValueTask UnFocus(ElementReference elementReference)
    {
        await _jSModule!.InvokeVoidAsync("unFocus", elementReference);
    }

    public async Task<Box> GetBoundingBox(ElementReference elementReference)
    {
        return await _jSModule!.InvokeAsync<Box>("BBox", elementReference);
    }

    public async Task CopyElementsAsync()
    {
        await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", string.Join("\n", MarkedShapes.Select(e => e.StoredHtml)));
    }

    public async Task PasteElementsAsync(ISVGElement SVGElement = null)
    {
        string clipboard = await JSRuntime.InvokeAsync<string>("navigator.clipboard.readText");
        List<string> elementsAsHtml = Elements.Select(e => e.StoredHtml).ToList();
        if (SVGElement != null)
        {
            int index = Elements.IndexOf(SVGElement);
            elementsAsHtml.Insert(index + 1, clipboard);
        }
        else
        {
            elementsAsHtml.Add(clipboard);
        }
        SelectedShapes.Clear();
        InputUpdated(string.Join("\n", elementsAsHtml));
    }
}