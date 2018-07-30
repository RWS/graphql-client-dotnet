package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class TaxonomyItemEdge
	{
		private ITaxonomyItem node;
		private String cursor;

		 public ITaxonomyItem getNode()
		 {
			 return node;
		 }
		 public void setNode(ITaxonomyItem node)
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
