package com.documentanalysis.visitors;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import com.documentanalysis.models.Section;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class KeywordSearchVisitor implements IDocumentVisitor {
    private final String keyword;
    private final List<String> keywordDetails = new ArrayList<>();
    private int totalMatches;

    public KeywordSearchVisitor(String keyword) {
        this.keyword = keyword == null ? "" : keyword.trim();
    }

    public String getKeyword() {
        return keyword;
    }

    public int getTotalMatches() {
        return totalMatches;
    }

    public List<String> getKeywordDetails() {
        return Collections.unmodifiableList(keywordDetails);
    }

    @Override
    public void visit(Document document) {
        // No direct search at document level.
    }

    @Override
    public void visit(Section section) {
        // No direct search at section level.
    }

    @Override
    public void visit(Paragraph paragraph) {
        if (keyword.isBlank() || paragraph.getText() == null || paragraph.getText().isBlank()) {
            return;
        }

        Pattern pattern = Pattern.compile("\\b" + Pattern.quote(keyword) + "\\b", Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(paragraph.getText());
        int matches = 0;
        while (matcher.find()) {
            matches++;
        }

        if (matches > 0) {
            totalMatches += matches;
            keywordDetails.add(matches + " lần trong đoạn: " + paragraph.getText().trim());
        }
    }
}
