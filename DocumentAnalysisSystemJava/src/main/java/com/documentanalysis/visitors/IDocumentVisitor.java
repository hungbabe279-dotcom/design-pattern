package com.documentanalysis.visitors;

import com.documentanalysis.models.Document;
import com.documentanalysis.models.Paragraph;
import com.documentanalysis.models.Section;

public interface IDocumentVisitor {
    void visit(Document document);
    void visit(Section section);
    void visit(Paragraph paragraph);
}
