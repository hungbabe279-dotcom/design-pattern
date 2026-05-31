using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentAnalysisSystem.Models;
using WordDocument = DocumentFormat.OpenXml.Wordprocessing.Document;
using ParagraphElement = DocumentFormat.OpenXml.Wordprocessing.Paragraph;

namespace DocumentAnalysisSystem.Readers
{
    public class DocxDocumentReader : IDocumentReader
    {
        public Document ReadDocument(string filePath)
        {
            var document = new Document(Path.GetFileName(filePath));

            using var wordDocument = WordprocessingDocument.Open(filePath, false);
            var body = wordDocument.MainDocumentPart?.Document.Body;
            if (body == null)
            {
                return document;
            }

            foreach (var paragraph in body.Elements<ParagraphElement>())
            {
                var text = paragraph.InnerText?.Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    document.Add(new Paragraph(text));
                }
            }

            return document;
        }
    }
}
