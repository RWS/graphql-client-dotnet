package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class KeywordConnection
	{
		private List<KeywordEdge> edges;

		 public List<KeywordEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<KeywordEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
