using Autofac;
using TfsLamp.HtmlRendering.Mapping;
using TfsLamp.HtmlRendering.Rendering;
using TfsLamp.Infrastructure.Rendering;

namespace TfsLamp.HtmlRendering.Registration
{
    public class HtmlRenderingRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HtmlStringRenderer>().As<IHtmlStringRenderer>();
            builder.RegisterType<Renderer>().As<IRenderer>();
            builder.RegisterType<WorkItemAssociationsToViewModelMapper>().As<IWorkItemAssociationsToViewModelMapper>();
            builder.RegisterType<WorkItemDescriptionGenerator>().As<IWorkItemDescriptionGenerator>();
            builder.RegisterType<ChangesetDescriptionGenerator>().As<IChangesetDescriptionGenerator>();
        }
    }
}