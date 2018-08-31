package com.sdl.web.pca.client.contentmodel;


import com.fasterxml.jackson.databind.annotation.JsonDeserialize;
/**
*An edge in a connection
*/
public class ItemEdge{

		@JsonDeserialize(as = Publication.class)
		private Item node;
		private String cursor;


		public Item getNode()
		{
			return node;
		}
		public void setNode(Item node)
		{
			this.node = node;
		}


		public String getCursor()
		{
			return cursor;
		}
		public void setCursor(String cursor)
		{
			this.cursor = cursor;
		}
	
}
