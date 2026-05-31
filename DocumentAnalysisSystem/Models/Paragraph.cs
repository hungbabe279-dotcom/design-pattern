using DocumentAnalysisSystem.Visitors;

namespace DocumentAnalysisSystem.Models
{
    public class Paragraph : DocumentComponent
    {
        public Paragraph(string text) : base("Paragraph")
        {
            Text = text;
        }

        public string Text { get; }

        public override IReadOnlyList<DocumentComponent> Children => new DocumentComponent[0];

        public override void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
