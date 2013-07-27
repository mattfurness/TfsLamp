using System;
using Machine.Fakes;
using Machine.Specifications;
using TfsLamp.HtmlRendering.Mapping;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Connection;

namespace TfsLamp.HtmlRendering.Tests.Mapping
{
    public class when_work_item_mapped_to_description : WithSubject<WorkItemDescriptionGenerator>
    {
        private static WorkItemDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsWorkItem
            {
                Id = 1,
                Description = "Description",
                Title = "Title",
                Type = "Type"
            });

        private It should_have_the_same_id = () => description.Id.ShouldEqual(1);
        private It should_have_the_same_description = () => description.Description.ShouldEqual("Description");
        private It should_have_a_title_with_a_type = () => description.Title.ShouldEqual("[Type]: Title");
    }

    public class when_work_item_mapped_with_a_url : WithSubject<WorkItemDescriptionGenerator>
    {
        private static WorkItemDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsWorkItem
            {
                Url = new Uri("http://127.0.0.1/")
            });

        private It should_have_a_string_representation_of_the_url = () => description.Url.ShouldEqual("http://127.0.0.1/");
    }

    public class when_work_item_mapped_with_no_url : WithSubject<WorkItemDescriptionGenerator>
    {
        private static WorkItemDescription description;

        private Because of = () => description = Subject.GenerateDescription(new TfsWorkItem
            {
            });

        private It should_have_a_string_representation_of_the_url = () => description.Url.ShouldBeEmpty();
    }
}