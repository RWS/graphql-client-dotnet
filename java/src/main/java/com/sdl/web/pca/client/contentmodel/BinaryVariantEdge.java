package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// An edge in a connection
	/// </summary>
	class BinaryVariantEdge
	{
		private BinaryVariant node;
		private String cursor;

		 public BinaryVariant getNode()
		 {
			 return node;
		 }
		 public void setNode(BinaryVariant node)
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
