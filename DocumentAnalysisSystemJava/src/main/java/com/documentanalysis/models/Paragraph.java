package com.documentanalysis.models;

import com.documentanalysis.visitors.IDocumentVisitor;

import java.util.Collections;
import java.util.List;

public class Paragraph extends DocumentComponent {
    private final String text;

    public Paragraph(String text) {
        super("Paragraph");
        this.text = text;
    }

    public String getText() {
        return text;
    }

    @Override
    public List<DocumentComponent> getChildren() {
        return Collections.emptyList();
    }

    @Override
    public void accept(IDocumentVisitor visitor) {
        visitor.visit(this);
    }
}
