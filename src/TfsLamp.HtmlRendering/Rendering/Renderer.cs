using System.IO;
using System.Text;
using TfsLamp.HtmlRendering.Mapping;
using TfsLamp.Infrastructure.ChangeInfo;
using TfsLamp.Infrastructure.Rendering;

namespace TfsLamp.HtmlRendering.Rendering
{
    public class Renderer : IRenderer
    {
        private readonly IWorkItemAssociationsToViewModelMapper _mapper;
        private readonly IHtmlStringRenderer _htmlStringRenderer;

        public Renderer(IWorkItemAssociationsToViewModelMapper mapper,
                        IHtmlStringRenderer htmlStringRenderer)
        {
            _mapper = mapper;
            _htmlStringRenderer = htmlStringRenderer;
        }

        public void Render(AllRelevantWorkItems workItems, string fileNameToRenderTo)
        {
            var viewModel = _mapper.Map(workItems);
            var hmtlString = _htmlStringRenderer.RenderHmtl(viewModel);
            WriteToFile(new UTF8Encoding(true).GetBytes(hmtlString), fileNameToRenderTo);
        }

        protected virtual void WriteToFile(byte[] bytes, string fileNameToRenderTo)
        {
            try
            {
                using(FileStream outputFile = File.Open(fileNameToRenderTo, FileMode.Create))
                {
                    outputFile.Write(bytes, 0, bytes.Length);
                }
            }
            catch (IOException ioException)
            {
                throw new RenderException(string.Format("There was a problem writing to the the file {0}", fileNameToRenderTo), ioException);
            }
        }
    }
}