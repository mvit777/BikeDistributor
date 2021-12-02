using BikeDistributor.Domain.Models;
using MV.Framework.interfaces;
using System.IO;

namespace BikeDistributor.renderers
{
    public class PdfRenderer : BaseRenderer, ITemplateRenderer
    {
        public PdfRenderer(Order order) : base(order)
        {

        }
        public string RenderTemplate(string outputFile = null)
        {
            HtmlRenderer renderer = new HtmlRenderer(_order);
            string html = renderer.RenderTemplate();
            //using (FileStream pdfDest = File.Open(outputFile, FileMode.OpenOrCreate))
            //{
            //    ConverterProperties converterProperties = new ConverterProperties();
            //    HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
            //}

            return html;
        }
    }
}
