using System.Text.RegularExpressions;
using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public class WordCountVisitor : IDocumentVisitor
    {
        public int WordCount { get; private set; }

        public void Visit(Document document)
        {
            // No additional word count logic required at document level.
        }

        public void Visit(Section section)
        {
            // Section titles are not counted as document words in this implementation.
        }

        public void Visit(Paragraph paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph.Text))
                return;

            var matches = Regex.Matches(paragraph.Text.Trim(), "\\b[\\w']+\\b");
            WordCount += matches.Count;
        }
    }
}
