using System.IO;
using System.Text;
using DocumentAnalysisSystem.Models;

namespace DocumentAnalysisSystem.Visitors
{
    public class ExportReportVisitor : IDocumentVisitor
    {
        private readonly StringBuilder _builder;

        public ExportReportVisitor(string analysisSummary)
        {
            _builder = new StringBuilder();
            _builder.AppendLine("==== Báo cáo phân tích tài liệu ====");
            _builder.AppendLine();
            _builder.AppendLine(analysisSummary);
            _builder.AppendLine();
            _builder.AppendLine("==== Nội dung tài liệu ====");
            _builder.AppendLine();
        }

        public void Visit(Document document)
        {
            // Root document node can be used for future metadata.
        }

        public void Visit(Section section)
        {
            if (!string.IsNullOrWhiteSpace(section.Title))
            {
                _builder.AppendLine(section.Title);
                _builder.AppendLine(new string('-', section.Title.Length));
            }
        }

        public void Visit(Paragraph paragraph)
        {
            _builder.AppendLine(paragraph.Text);
            _builder.AppendLine();
        }

        public void SaveReport(string filePath)
        {
            File.WriteAllText(filePath, _builder.ToString());
        }
    }
}
