using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Readers
{
    public interface IDocumentReader
    {
        Document ReadDocument(string filePath);
    }
}
