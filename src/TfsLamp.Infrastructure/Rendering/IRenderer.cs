using TfsLamp.Infrastructure.ChangeInfo;

namespace TfsLamp.Infrastructure.Rendering
{
    public interface IRenderer
    {
        void Render(AllRelevantWorkItems workItems, string fileNameToRenderTo);
    }
}