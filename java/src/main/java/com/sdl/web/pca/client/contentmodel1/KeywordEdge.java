package com.sdl.web.pca.client.contentmodel1;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class KeywordEdge
	{
		private Keyword node;
		private String cursor;

		 public Keyword getNode()
		 {
			 return node;
		 }
		 public void setNode(Keyword node)
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
