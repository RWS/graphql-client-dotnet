package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class TaxonomyItemConnection
	{
		private List<TaxonomyItemEdge> edges;

		 public List<TaxonomyItemEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<TaxonomyItemEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
