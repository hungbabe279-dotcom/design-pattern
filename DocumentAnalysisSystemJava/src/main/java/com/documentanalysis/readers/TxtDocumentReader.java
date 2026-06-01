package com.documentanalysis.readers;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;

import java.nio.file.Files;
import java.nio.file.Paths;

public class TxtDocumentReader implements IDocumentReader {
    @Override
    public Document readDocument(String filePath) throws Exception {
        String content = Files.readString(Paths.get(filePath));
        Document document = new Document(Paths.get(filePath).getFileName().toString());

        String[] paragraphs = content.split("(?:\r\n){2,}|(?:\n){2,}");
        for (String paragraphText : paragraphs) {
            String text = paragraphText.trim();
            if (!text.isEmpty()) {
                document.add(new Paragraph(text));
            }
        }

        return document;
    }
}
