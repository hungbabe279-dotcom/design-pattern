package com.documentanalysis.readers;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import org.apache.poi.xwpf.usermodel.XWPFDocument;
import org.apache.poi.xwpf.usermodel.XWPFParagraph;

import java.io.FileInputStream;
import java.nio.file.Paths;

public class DocxDocumentReader implements IDocumentReader {
    @Override
    public Document readDocument(String filePath) throws Exception {
        Document document = new Document(Paths.get(filePath).getFileName().toString());

        try (FileInputStream fis = new FileInputStream(filePath);
             XWPFDocument xwpfDocument = new XWPFDocument(fis)) {
            for (XWPFParagraph paragraph : xwpfDocument.getParagraphs()) {
                String text = paragraph.getText();
                if (text != null) {
                    text = text.trim();
                }
                if (text != null && !text.isEmpty()) {
                    document.add(new Paragraph(text));
                }
            }
        }

        return document;
    }
}
