package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class PageEdge
	{
		private Page node;
		private String cursor;

		 public Page getNode()
		 {
			 return node;
		 }
		 public void setNode(Page node)
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
