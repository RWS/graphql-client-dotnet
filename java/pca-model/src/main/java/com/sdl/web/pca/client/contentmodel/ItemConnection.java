package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class ItemConnection{
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
