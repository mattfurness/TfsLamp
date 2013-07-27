using TfsLamp.HtmlRendering.ViewModel;

namespace TfsLamp.HtmlRendering.Rendering
{
    public interface IHtmlStringRenderer
    {
        string RenderHmtl(ChangesViewModel viewModel);
    }
}