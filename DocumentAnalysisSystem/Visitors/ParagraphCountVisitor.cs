using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public class ParagraphCountVisitor : IDocumentVisitor
    {
        public int ParagraphCount { get; private set; }

        public void Visit(Document document)
        {
            // No count at document level.
        }

        public void Visit(Section section)
        {
            // No count at section level.
        }

        public void Visit(Paragraph paragraph)
        {
            ParagraphCount++;
        }
    }
}
