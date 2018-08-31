package com.sdl.web.pca.client.contentmodel;


/**
*An edge in a connection
*/
public class KeywordEdge {
		private Keyword node;
		private String cursor;


		public Keyword getNode(){
			return node;
		}
		public void setNode(Keyword node){
			this.node = node;
		}


		public String getCursor(){
			return cursor;
		}
		public void setCursor(String cursor){
			this.cursor = cursor;
		}
	
}
