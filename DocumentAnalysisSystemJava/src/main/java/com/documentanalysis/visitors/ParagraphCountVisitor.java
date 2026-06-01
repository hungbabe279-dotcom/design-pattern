package com.documentanalysis.visitors;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import com.documentanalysis.models.Section;

public class ParagraphCountVisitor implements IDocumentVisitor {
    private int paragraphCount;

    public int getParagraphCount() {
        return paragraphCount;
    }

    @Override
    public void visit(Document document) {
        // No direct count at document level.
    }

    @Override
    public void visit(Section section) {
        // No direct count at section level.
    }

    @Override
    public void visit(Paragraph paragraph) {
        paragraphCount++;
    }
}
