using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Mapping
{
    public class WorkItemDescriptionGenerator : IWorkItemDescriptionGenerator
    {
        public WorkItemDescription GenerateDescription(TfsWorkItem workItem)
        {
            return new WorkItemDescription
                {
                    Id = workItem.Id,
                    Description = workItem.Description,
                    Title = string.Format("[{0}]: {1}", workItem.Type, workItem.Title),
                    Url = workItem.Url != null ? workItem.Url.ToString() : string.Empty
                };
        }
    }
}