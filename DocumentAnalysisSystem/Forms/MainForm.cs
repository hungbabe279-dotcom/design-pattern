using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentAnalysisSystem.Models;
using DocumentAnalysisSystem.Readers;
using DocumentAnalysisSystem.Visitors;

namespace DocumentAnalysisSystem.Forms
{
    public partial class MainForm : Form
    {
        private Document? _currentDocument;
        private string _currentFilePath = string.Empty;
        private string _analysisSummary = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Title = "Chọn tài liệu";
            dialog.Filter = "Text files (*.txt)|*.txt|Word files (*.docx)|*.docx|PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            _currentFilePath = dialog.FileName;
            try
            {
                var reader = DocumentReaderFactory.CreateReader(_currentFilePath);
                _currentDocument = reader.ReadDocument(_currentFilePath);
                txtContent.Text = GetDocumentText(_currentDocument);
                txtResults.Text = string.Empty;
                _analysisSummary = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDocumentText(DocumentComponent component)
        {
            var builder = new StringBuilder();
            if (component is Paragraph paragraph)
            {
                builder.AppendLine(paragraph.Text);
            }
            else if (component is Section section)
            {
                if (!string.IsNullOrWhiteSpace(section.Title))
                {
                    builder.AppendLine(section.Title);
                }

                foreach (var child in section.Children)
                {
                    builder.AppendLine(GetDocumentText(child));
                }
            }
            else if (component is Document document)
            {
                foreach (var child in document.Children)
                {
                    builder.AppendLine(GetDocumentText(child));
                }
            }

            return builder.ToString().TrimEnd();
        }

        private void btnAnalyze_Click(object? sender, EventArgs e)
        {
            if (_currentDocument == null)
            {
                MessageBox.Show("Vui lòng chọn file trước khi phân tích.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var wordVisitor = new WordCountVisitor();
            var charVisitor = new CharacterCountVisitor();
            var paragraphVisitor = new ParagraphCountVisitor();
            var keywordVisitor = new KeywordSearchVisitor(txtKeyword.Text.Trim());

            _currentDocument.Accept(wordVisitor);
            _currentDocument.Accept(charVisitor);
            _currentDocument.Accept(paragraphVisitor);
            _currentDocument.Accept(keywordVisitor);

            _analysisSummary = BuildSummary(wordVisitor, charVisitor, paragraphVisitor, keywordVisitor);
            txtResults.Text = _analysisSummary;
        }

        private static string BuildSummary(WordCountVisitor words, CharacterCountVisitor characters, ParagraphCountVisitor paragraphs, KeywordSearchVisitor keywordSearch)
        {
            var summary = new StringBuilder();
            summary.AppendLine("Kết quả phân tích tài liệu:");
            summary.AppendLine($"- Số từ: {words.WordCount}");
            summary.AppendLine($"- Số ký tự: {characters.CharacterCount}");
            summary.AppendLine($"- Số đoạn/paragrap: {paragraphs.ParagraphCount}");
            summary.AppendLine($"- Từ khóa: {(string.IsNullOrWhiteSpace(keywordSearch.Keyword) ? "(chưa nhập)" : keywordSearch.Keyword)}");
            summary.AppendLine($"- Số lần xuất hiện: {keywordSearch.TotalMatches}");
            if (keywordSearch.KeywordDetails.Any())
            {
                summary.AppendLine("- Chi tiết tìm kiếm:");
                foreach (var detail in keywordSearch.KeywordDetails)
                {
                    summary.AppendLine($"  * {detail}");
                }
            }
            return summary.ToString().TrimEnd();
        }

        private void btnExportReport_Click(object? sender, EventArgs e)
        {
            if (_currentDocument == null)
            {
                MessageBox.Show("Vui lòng chọn file trước khi xuất báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(_analysisSummary))
            {
                MessageBox.Show("Bạn cần phân tích tài liệu trước khi xuất báo cáo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog();
            dialog.Title = "Lưu báo cáo";
            dialog.Filter = "Text files (*.txt)|*.txt";
            dialog.FileName = Path.GetFileNameWithoutExtension(_currentFilePath) + "_report.txt";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                var reportVisitor = new ExportReportVisitor(_analysisSummary);
                _currentDocument.Accept(reportVisitor);
                reportVisitor.SaveReport(dialog.FileName);
                MessageBox.Show("Xuất báo cáo thành công.", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Document Analysis System";
            this.ClientSize = new System.Drawing.Size(960, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            var btnSelectFile = new Button
            {
                Name = "btnSelectFile",
                Text = "Chọn file",
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(120, 35)
            };
            btnSelectFile.Click += btnSelectFile_Click;

            var lblKeyword = new Label
            {
                Text = "Từ khóa:",
                Location = new System.Drawing.Point(20, 70),
                AutoSize = true
            };

            txtKeyword = new TextBox
            {
                Name = "txtKeyword",
                Location = new System.Drawing.Point(90, 68),
                Size = new System.Drawing.Size(320, 27)
            };

            var btnAnalyze = new Button
            {
                Name = "btnAnalyze",
                Text = "Phân tích",
                Location = new System.Drawing.Point(430, 20),
                Size = new System.Drawing.Size(120, 35)
            };
            btnAnalyze.Click += btnAnalyze_Click;

            var btnExportReport = new Button
            {
                Name = "btnExportReport",
                Text = "Xuất báo cáo",
                Location = new System.Drawing.Point(430, 65),
                Size = new System.Drawing.Size(120, 35)
            };
            btnExportReport.Click += btnExportReport_Click;

            var lblContent = new Label
            {
                Text = "Nội dung tài liệu:",
                Location = new System.Drawing.Point(20, 110),
                AutoSize = true
            };

            txtContent = new TextBox
            {
                Name = "txtContent",
                Location = new System.Drawing.Point(20, 135),
                Size = new System.Drawing.Size(920, 380),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                WordWrap = false,
                ReadOnly = true
            };

            var lblResults = new Label
            {
                Text = "Kết quả phân tích:",
                Location = new System.Drawing.Point(20, 530),
                AutoSize = true
            };

            txtResults = new TextBox
            {
                Name = "txtResults",
                Location = new System.Drawing.Point(20, 555),
                Size = new System.Drawing.Size(920, 140),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };

            this.Controls.Add(btnSelectFile);
            this.Controls.Add(lblKeyword);
            this.Controls.Add(txtKeyword);
            this.Controls.Add(btnAnalyze);
            this.Controls.Add(btnExportReport);
            this.Controls.Add(lblContent);
            this.Controls.Add(txtContent);
            this.Controls.Add(lblResults);
            this.Controls.Add(txtResults);
        }

        private TextBox txtContent = null!;
        private TextBox txtKeyword = null!;
        private TextBox txtResults = null!;
    }
}
