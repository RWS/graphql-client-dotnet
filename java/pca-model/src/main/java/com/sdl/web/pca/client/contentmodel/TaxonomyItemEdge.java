package com.sdl.web.pca.client.contentmodel;


/**
*An edge in a connection
*/
public class TaxonomyItemEdge {
		private TaxonomyItem node;
		private String cursor;


		public TaxonomyItem getNode()
		{
			return node;
		}
		public void setNode(TaxonomyItem node)
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
