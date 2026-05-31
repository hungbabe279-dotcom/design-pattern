using System;
using System.IO;

namespace DocumentAnalysisSystem.Readers
{
    public static class DocumentReaderFactory
    {
        public static IDocumentReader CreateReader(string filePath)
        {
            var extension = Path.GetExtension(filePath)?.ToLowerInvariant();
            return extension switch
            {
                ".txt" => new TxtDocumentReader(),
                ".docx" => new DocxDocumentReader(),
                ".pdf" => new PdfDocumentReader(),
                _ => throw new NotSupportedException("Định dạng file không được hỗ trợ."),
            };
        }
    }
}
