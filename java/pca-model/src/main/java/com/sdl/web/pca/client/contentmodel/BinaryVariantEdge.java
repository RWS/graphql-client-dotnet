package com.sdl.web.pca.client.contentmodel;


/**
*An edge in a connection
*/
public class BinaryVariantEdge {
		private BinaryVariant node;
		private String cursor;


		public BinaryVariant getNode(){
			return node;
		}
		public void setNode(BinaryVariant node){
			this.node = node;
		}


		public String getCursor(){
			return cursor;
		}
		public void setCursor(String cursor){
			this.cursor = cursor;
		}
	
}
