using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClickNCheck.Services
{
    public class PDFGenerator
    {
        public byte[] GeneratePDF()
        {
            // instantiate a html to pdf converter object 
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            string path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Files\RecruiterEmail.html");
            string htmlstring = System.IO.File.ReadAllText(path);
            // create a new pdf document converting an url 
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlstring);

            // save pdf document 
            byte[] pdf = doc.Save();

            // close pdf document 
            doc.Close();

            return pdf;
        }
    }
}
