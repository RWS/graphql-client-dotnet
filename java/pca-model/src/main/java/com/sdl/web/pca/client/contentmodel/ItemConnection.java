package com.sdl.web.pca.client.contentmodel;

import com.fasterxml.jackson.annotation.JsonRootName;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	@JsonRootName(value = "items")
	public class ItemConnection
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
