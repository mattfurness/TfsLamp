using System;
using Machine.Fakes;
using Machine.Specifications;
using TfsLamp.HtmlRendering.Mapping;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Tests.Mapping
{
    public class when_changeset_mapped_to_description : WithSubject<ChangesetDescriptionGenerator>
    {
        private static ChangesetDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsChangeset
            {
                Id = 1,
                Author = "Author",
                Date = new DateTime(2001,1,1),
                Comment = "Comment",
            });

        private It should_have_the_same_id = () => description.Id.ShouldEqual(1);
        private It should_have_a_description_that_contains_the_date_and_comment_and_auther = () => description.Description.ShouldEqual("Comment on 01/01/2001 by Author.");
    }

    public class when_changeset_mapped_with_a_uri : WithSubject<ChangesetDescriptionGenerator>
    {
        private static ChangesetDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsChangeset
            {
                Url = new Uri("http://127.0.0.1/")
            });

        private It should_have_a_string_representation_of_the_uri = () => description.Url.ShouldEqual("http://127.0.0.1/");
    }

    public class when_changeset_mapped_with_no_uri : WithSubject<ChangesetDescriptionGenerator>
    {
        private static ChangesetDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsChangeset
            {
            });

        private It should_have_a_string_representation_of_the_uri = () => description.Url.ShouldBeEmpty();
    }
}