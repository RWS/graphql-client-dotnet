package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class CustomMetaEdge
	{
		private CustomMeta node;
		private String cursor;

		 public CustomMeta getNode()
		 {
			 return node;
		 }
		 public void setNode(CustomMeta node)
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
