package com.documentanalysis;

import com.documentanalysis.models.Document;
import com.documentanalysis.readers.DocumentReaderFactory;
import com.documentanalysis.readers.IDocumentReader;
import com.documentanalysis.visitors.KeywordSearchVisitor;
import com.documentanalysis.visitors.ParagraphCountVisitor;
import com.documentanalysis.visitors.WordCountVisitor;

import java.nio.file.Path;
import java.nio.file.Paths;

public class Main {
    public static void main(String[] args) {
        if (args.length < 1) {
            System.out.println("Usage: java -jar document-analysis-system.jar <file-path> [keyword]");
            return;
        }

        Path filePath = Paths.get(args[0]);
        String keyword = args.length >= 2 ? args[1] : null;

        try {
            IDocumentReader reader = DocumentReaderFactory.createReader(filePath.toString());
            Document document = reader.readDocument(filePath.toString());

            WordCountVisitor wordCountVisitor = new WordCountVisitor();
            ParagraphCountVisitor paragraphCountVisitor = new ParagraphCountVisitor();

            document.accept(wordCountVisitor);
            document.accept(paragraphCountVisitor);

            System.out.println("Document: " + document.getName());
            System.out.println("Paragraph count: " + paragraphCountVisitor.getParagraphCount());
            System.out.println("Word count: " + wordCountVisitor.getWordCount());

            if (keyword != null && !keyword.isBlank()) {
                KeywordSearchVisitor keywordSearchVisitor = new KeywordSearchVisitor(keyword);
                document.accept(keywordSearchVisitor);
                System.out.println("Keyword: " + keyword);
                System.out.println("Total matches: " + keywordSearchVisitor.getTotalMatches());
                keywordSearchVisitor.getKeywordDetails().forEach(System.out::println);
            }
        } catch (Exception e) {
            System.err.println("Error: " + e.getMessage());
            e.printStackTrace();
        }
    }
}
