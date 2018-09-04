package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	public class PublicationConnection
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
