package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class ItemConnection
	{
		private List<ItemEdge> edges;

		 public List<ItemEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<ItemEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
