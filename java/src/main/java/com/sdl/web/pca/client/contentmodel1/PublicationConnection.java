package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class PublicationConnection
	{
		private List<PublicationEdge> edges;

		 public List<PublicationEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<PublicationEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
