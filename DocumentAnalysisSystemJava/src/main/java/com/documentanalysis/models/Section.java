package com.documentanalysis.models;

import com.documentanalysis.visitors.IDocumentVisitor;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Section extends DocumentComponent {
    private final String title;
    private final List<DocumentComponent> children = new ArrayList<>();

    public Section(String title) {
        super(title);
        this.title = title;
    }

    public String getTitle() {
        return title;
    }

    @Override
    public List<DocumentComponent> getChildren() {
        return Collections.unmodifiableList(children);
    }

    public void add(DocumentComponent component) {
        children.add(component);
    }

    @Override
    public void accept(IDocumentVisitor visitor) {
        visitor.visit(this);
        for (DocumentComponent child : children) {
            child.accept(visitor);
        }
    }
}
