package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class CustomMetaConnection {
		private List<CustomMetaEdge> edges;


		public List<CustomMetaEdge> getEdges(){
			return edges;
		}
		public void setEdges(List<CustomMetaEdge> edges){
			this.edges = edges;
		}
	
}
