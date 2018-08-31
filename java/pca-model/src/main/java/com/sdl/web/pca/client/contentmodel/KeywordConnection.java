package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*A connection to a list of items.
*/
public class KeywordConnection {
		private List<KeywordEdge> edges;


		public List<KeywordEdge> getEdges(){
			return edges;
		}
		public void setEdges(List<KeywordEdge> edges){
			this.edges = edges;
		}
	
}
