package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class PageConnection
	{
		private List<PageEdge> edges;

		 public List<PageEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<PageEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
