package com.documentanalysis.models;

import com.documentanalysis.visitors.IDocumentVisitor;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Document extends DocumentComponent {
    private final List<DocumentComponent> children = new ArrayList<>();

    public Document(String name) {
        super(name);
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
