package com.documentanalysis.readers;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.text.PDFTextStripper;

import java.io.File;
import java.nio.file.Paths;

public class PdfDocumentReader implements IDocumentReader {
    @Override
    public Document readDocument(String filePath) throws Exception {
        Document document = new Document(Paths.get(filePath).getFileName().toString());

        try (PDDocument pdf = PDDocument.load(new File(filePath))) {
            PDFTextStripper stripper = new PDFTextStripper();
            String text = stripper.getText(pdf);
            String[] paragraphs = text.split("(?:\r\n){2,}|(?:\n){2,}");

            for (String paragraphText : paragraphs) {
                String trimmed = paragraphText.trim();
                if (!trimmed.isEmpty()) {
                    document.add(new Paragraph(trimmed));
                }
            }
        }

        return document;
    }
}
