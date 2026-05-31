using System.IO;
using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Readers
{
    public class TxtDocumentReader : IDocumentReader
    {
        public Document ReadDocument(string filePath)
        {
            var content = File.ReadAllText(filePath);
            var document = new Document(Path.GetFileName(filePath));
            var paragraphs = content.Split(new[] { "\r\n\r\n", "\n\n" }, System.StringSplitOptions.RemoveEmptyEntries);

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
