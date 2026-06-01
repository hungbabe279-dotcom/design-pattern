package com.documentanalysis.visitors;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import com.documentanalysis.models.Section;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class WordCountVisitor implements IDocumentVisitor {
    private static final Pattern WORD_PATTERN = Pattern.compile("\\b[\\w']+\\b");
    private int wordCount;

    public int getWordCount() {
        return wordCount;
    }

    @Override
    public void visit(Document document) {
        // Document-level nodes do not add to the word count.
    }

    @Override
    public void visit(Section section) {
        // Section titles are not counted in this implementation.
    }

    @Override
    public void visit(Paragraph paragraph) {
        if (paragraph.getText() == null || paragraph.getText().isBlank()) {
            return;
        }

        Matcher matcher = WORD_PATTERN.matcher(paragraph.getText().trim());
        while (matcher.find()) {
            wordCount++;
        }
    }
}
