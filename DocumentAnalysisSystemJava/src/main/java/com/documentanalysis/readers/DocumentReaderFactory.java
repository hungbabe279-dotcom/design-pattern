package com.documentanalysis.readers;

import java.nio.file.Path;
import java.nio.file.Paths;

public class DocumentReaderFactory {
    public static IDocumentReader createReader(String filePath) {
        String extension = getExtension(filePath).toLowerCase();
        return switch (extension) {
            case ".txt" -> new TxtDocumentReader();
            case ".docx" -> new DocxDocumentReader();
            case ".pdf" -> new PdfDocumentReader();
            default -> throw new IllegalArgumentException("Unsupported file format: " + extension);
        };
    }

    private static String getExtension(String filePath) {
        Path path = Paths.get(filePath);
        String fileName = path.getFileName().toString();
        int dotIndex = fileName.lastIndexOf('.');
        if (dotIndex == -1) {
            return "";
        }
        return fileName.substring(dotIndex);
    }
}
