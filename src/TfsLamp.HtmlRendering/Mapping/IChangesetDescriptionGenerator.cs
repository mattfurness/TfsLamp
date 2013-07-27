using Microsoft.TeamFoundation.VersionControl.Client;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Mapping
{
    public interface IChangesetDescriptionGenerator
    {
        ChangesetDescription GenerateDescription(TfsChangeset changeset);
    }
}