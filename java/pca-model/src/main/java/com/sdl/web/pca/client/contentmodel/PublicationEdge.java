package com.sdl.web.pca.client.contentmodel;


/**
*An edge in a connection
*/
public class PublicationEdge {
		private Publication node;
		private String cursor;


		public Publication getNode(){
			return node;
		}
		public void setNode(Publication node){
			this.node = node;
		}


		public String getCursor(){
			return cursor;
		}
		public void setCursor(String cursor){
			this.cursor = cursor;
		}
	
}
