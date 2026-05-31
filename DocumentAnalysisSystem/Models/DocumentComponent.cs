using System.Collections.Generic;
using DocumentAnalysisSystem.Visitors;

namespace DocumentAnalysisSystem.Models
{
    public abstract class DocumentComponent
    {
        protected DocumentComponent(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract IReadOnlyList<DocumentComponent> Children { get; }

        public abstract void Accept(IDocumentVisitor visitor);
    }
}
