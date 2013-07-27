using System;
using System.IO;
using System.Reflection;
using TfsLamp.HtmlRendering.ViewModel;
using TfsLamp.Infrastructure.Rendering;
using Westwind.RazorHosting;

namespace TfsLamp.HtmlRendering.Rendering
{
    public class HtmlStringRenderer : IHtmlStringRenderer
    {
        public string RenderHmtl(ChangesViewModel viewModel)
        {
            var binaryFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var host = new RazorFolderHostContainer
                {
                    TemplatePath = binaryFolder + @"\Templates\",
                    BaseBinaryFolder = binaryFolder,
                    UseAppDomain = false
                };
            host.Configuration.CompileToMemory = true;
            host.Configuration.TempAssemblyPath = Environment.CurrentDirectory;
            host.AddAssemblyFromType(typeof(ChangesViewModel));

            host.Start();
            string result = host.RenderTemplate("~/TfsLamp.cshtml", viewModel);
            host.Stop();

            if (result == null)
                throw new RenderException(host.ErrorMessage);

            return result;
        }
    }
}