using System.Collections.Generic;
using System.Text.RegularExpressions;
using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public class KeywordSearchVisitor : IDocumentVisitor
    {
        public KeywordSearchVisitor(string keyword)
        {
            Keyword = keyword.Trim();
        }

        public string Keyword { get; }
        public int TotalMatches { get; private set; }
        public IReadOnlyList<string> KeywordDetails => _keywordDetails.AsReadOnly();
        private readonly List<string> _keywordDetails = new();

        public void Visit(Document document)
        {
            // No action needed at document level.
        }

        public void Visit(Section section)
        {
            // No direct search inside section nodes.
        }

        public void Visit(Paragraph paragraph)
        {
            if (string.IsNullOrWhiteSpace(Keyword) || string.IsNullOrWhiteSpace(paragraph.Text))
                return;

            var pattern = $"\\b{Regex.Escape(Keyword)}\\b";
            var matches = Regex.Matches(paragraph.Text, pattern, RegexOptions.IgnoreCase);
            if (matches.Count > 0)
            {
                TotalMatches += matches.Count;
                _keywordDetails.Add($"{matches.Count} lần trong đoạn: {paragraph.Text.Trim()}");
            }
        }
    }
}
