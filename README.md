# Document Analysis System

Ứng dụng `DocumentAnalysisSystem` là một hệ thống phân tích tài liệu đơn giản viết bằng C# WinForms.
Nó hỗ trợ đọc tài liệu từ các định dạng `.txt`, `.docx` và `.pdf`, sau đó thực hiện phân tích cơ bản như đếm số từ, số ký tự, số đoạn và tìm kiếm từ khóa.

## Tính năng

- Đọc file tài liệu:
  - `.txt`
  - `.docx`
  - `.pdf`
- Hiển thị nội dung tài liệu trong giao diện.
- Phân tích tài liệu:
  - Đếm số từ
  - Đếm số ký tự
  - Đếm số đoạn/paragrap
  - Tìm kiếm từ khóa và thống kê số lần xuất hiện
- Xuất báo cáo phân tích ra file `.txt`.

## Kiến trúc chính

Dự án sử dụng mô hình thiết kế `Visitor` cùng với `Composite` cho cấu trúc tài liệu.

- `Models/`
  - `Document`, `Section`, `Paragraph`, `DocumentComponent`
- `Visitors/`
  - `WordCountVisitor`, `CharacterCountVisitor`, `ParagraphCountVisitor`, `KeywordSearchVisitor`, `ExportReportVisitor`
- `Readers/`
  - `IDocumentReader`, `TxtDocumentReader`, `DocxDocumentReader`, `PdfDocumentReader`, `DocumentReaderFactory`
- `Forms/MainForm.cs`
  - Giao diện Windows Forms điều khiển luồng chọn file, phân tích và xuất báo cáo.

## Công nghệ

- .NET 6.0
- Windows Forms
- `DocumentFormat.OpenXml` để đọc file Word `.docx`
- `UglyToad.PdfPig` để trích xuất văn bản từ file PDF

## Cài đặt và chạy

1. Mở solution `DocumentAnalysisSystem.sln` bằng Visual Studio.
2. Khôi phục gói NuGet nếu cần.
3. Chọn cấu hình `Debug` và `DocumentAnalysisSystem` làm startup project.
4. Chạy ứng dụng.

Hoặc dùng dòng lệnh:

```powershell
cd "c:\Users\damin\OneDrive\Documents\mtkpm\design pattern\DocumentAnalysisSystem"
dotnet restore
dotnet run
```

> Lưu ý: ứng dụng chạy trên Windows vì sử dụng Windows Forms và `net6.0-windows`.

## Sử dụng

1. Nhấn `Chọn file` để mở tài liệu `.txt`, `.docx` hoặc `.pdf`.
2. Nội dung tài liệu sẽ hiển thị ở khung `Nội dung tài liệu`.
3. Nhập từ khóa (nếu muốn) để tìm kiếm.
4. Nhấn `Phân tích` để xem kết quả.
5. Nhấn `Xuất báo cáo` để lưu báo cáo phân tích ra file `.txt`.

## Mở rộng

Có thể mở rộng dự án bằng cách:

- Thêm hỗ trợ đọc thêm định dạng file khác.
- Cải thiện cấu trúc tài liệu để nhận biết section và tiêu đề rõ ràng.
- Bổ sung visitor mới cho phân tích nâng cao (đếm câu, phân tích tần suất từ, thống kê ngữ pháp, v.v.).

## Ghi chú

- File `.docx` và `.pdf` được chuyển thành các đoạn văn (`Paragraph`) trước khi phân tích.
- Báo cáo xuất ra gồm tổng quan kết quả phân tích và nội dung tài liệu.
