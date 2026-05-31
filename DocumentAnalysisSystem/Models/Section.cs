using System.Collections.Generic;
using DocumentAnalysisSystem.Visitors;

namespace DocumentAnalysisSystem.Models
{
    public class Section : DocumentComponent
    {
        private readonly List<DocumentComponent> _children = new();

        public Section(string title) : base(title)
        {
            Title = title;
        }

        public string Title { get; }

        public override IReadOnlyList<DocumentComponent> Children => _children.AsReadOnly();

        public void Add(DocumentComponent component)
        {
            _children.Add(component);
        }

        public override void Accept(IDocumentVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in _children)
            {
                child.Accept(visitor);
            }
        }
    }
}
