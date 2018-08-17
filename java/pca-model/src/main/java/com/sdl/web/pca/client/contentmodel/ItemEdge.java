package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class ItemEdge
	{
		private Node node;
		private String cursor;

		 public Node getNode()
		 {
			 return node;
		 }
		 public void setNode(Node node)
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
