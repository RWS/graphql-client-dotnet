package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class PageConnection{
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
