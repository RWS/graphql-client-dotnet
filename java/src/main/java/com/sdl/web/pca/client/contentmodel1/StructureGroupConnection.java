package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// A connection to a list of items.
	/// </summary>
	class StructureGroupConnection
	{
		private List<StructureGroupEdge> edges;

		 public List<StructureGroupEdge> getEdges()
		 {
			 return edges;
		 }
		 public void setEdges(List<StructureGroupEdge> edges)
		 {
			 this.edges = edges;
		 }
	}
