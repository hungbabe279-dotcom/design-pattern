package com.documentanalysis.readers;

import com.documentanalysis.models.Document;

public interface IDocumentReader {
    Document readDocument(String filePath) throws Exception;
}
