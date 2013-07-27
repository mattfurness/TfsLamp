using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Mapping
{
    public interface IWorkItemDescriptionGenerator
    {
        WorkItemDescription GenerateDescription(TfsWorkItem workItem);
    }
}