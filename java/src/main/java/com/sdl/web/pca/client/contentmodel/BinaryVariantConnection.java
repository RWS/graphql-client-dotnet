package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/// <summary>
/// A connection to a list of items.
/// </summary>
class BinaryVariantConnection
{
    private List<BinaryVariantEdge> edges;

     public List<BinaryVariantEdge> getEdges()
     {
         return edges;
     }
     public void setEdges(List<BinaryVariantEdge> edges)
     {
         this.edges = edges;
     }
}
