package com.sdl.web.pca.client.contentmodel;


/**
*An edge in a connection
*/
public class ItemEdge{
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
