using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public class CharacterCountVisitor : IDocumentVisitor
    {
        public int CharacterCount { get; private set; }

        public void Visit(Document document)
        {
            // Document node itself does not contribute additional text beyond children.
        }

        public void Visit(Section section)
        {
            // Include section title characters if desired, but current design ignores section titles.
        }

        public void Visit(Paragraph paragraph)
        {
            if (paragraph.Text == null)
                return;

            CharacterCount += paragraph.Text.Length;
        }
    }
}
