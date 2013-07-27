using Microsoft.TeamFoundation.VersionControl.Client;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Mapping
{
    public class ChangesetDescriptionGenerator : IChangesetDescriptionGenerator
    {
        public ChangesetDescription GenerateDescription(TfsChangeset changeset)
        {
            var description = string.Format("{0} on {1:dd/MM/yyyy} by {2}.", changeset.Comment, changeset.Date, changeset.Author);

            return new ChangesetDescription
                {
                    Description = description,
                    Id = changeset.Id,
                    Url = changeset.Url != null ? changeset.Url.ToString() : string.Empty
                };
        }
    }
}