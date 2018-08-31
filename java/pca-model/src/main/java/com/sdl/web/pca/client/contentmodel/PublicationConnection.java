package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class PublicationConnection {
		private List<PublicationEdge> edges;


		public List<PublicationEdge> getEdges(){
			return edges;
		}
		public void setEdges(List<PublicationEdge> edges){
			this.edges = edges;
		}
	
}
