using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public interface IDocumentVisitor
    {
        void Visit(Document document);
        void Visit(Section section);
        void Visit(Paragraph paragraph);
    }
}
