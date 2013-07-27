using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.ChangeInfo;

namespace TfsLamp.HtmlRendering.Mapping
{
    public interface IWorkItemAssociationsToViewModelMapper
    {
        ChangesViewModel Map(AllRelevantWorkItems allRelevantAssociatedWorkItems);
    }
}