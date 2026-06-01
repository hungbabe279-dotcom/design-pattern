package com.documentanalysis.models;

import java.util.List;

public abstract class DocumentComponent {
    private final String name;

    protected DocumentComponent(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public abstract List<DocumentComponent> getChildren();

    public abstract void accept(com.documentanalysis.visitors.IDocumentVisitor visitor);
}
