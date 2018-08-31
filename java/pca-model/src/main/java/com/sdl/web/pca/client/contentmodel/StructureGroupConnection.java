package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class StructureGroupConnection {
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
