using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Drawing.Text;
using System.IO;

namespace WebEnviroment
{
    public class EnviromentHelper
    {
        public PrivateFontCollection FontCollection { get; set; }
        public FontFamily[] FontFamilies { get => FontCollection.Families; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnviromentHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            FontCollection = new PrivateFontCollection();
            string webRootPath = _webHostEnvironment.WebRootPath;

            // get all font in directory ReportFont
            string path = Path.Combine(webRootPath, "ReportFonts");
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                FontCollection.AddFontFile(file);
            }
        }
    }
}
