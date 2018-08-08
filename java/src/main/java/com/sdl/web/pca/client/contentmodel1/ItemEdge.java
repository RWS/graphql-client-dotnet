package com.sdl.web.pca.client.contentmodel1;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class ItemEdge
	{
		private IItem node;
		private String cursor;

		 public IItem getNode()
		 {
			 return node;
		 }
		 public void setNode(IItem node)
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
