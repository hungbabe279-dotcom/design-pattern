using System.IO;
using System.Linq;
using DocumentAnalysisSystem.Models;
using UglyToad.PdfPig;

namespace DocumentAnalysisSystem.Readers
{
    public class PdfDocumentReader : IDocumentReader
    {
        public Document ReadDocument(string filePath)
        {
            var document = new Document(Path.GetFileName(filePath));
            using var pdf = PdfDocument.Open(filePath);

            var textBuilder = new System.Text.StringBuilder();
            foreach (var page in pdf.GetPages())
            {
                textBuilder.AppendLine(page.Text);
                textBuilder.AppendLine();
            }

            var paragraphs = textBuilder.ToString().Split(new[] { "\r\n\r\n", "\n\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var paragraphText in paragraphs)
            {
                var text = paragraphText.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    document.Add(new Paragraph(text));
                }
            }

            return document;
        }
    }
}
