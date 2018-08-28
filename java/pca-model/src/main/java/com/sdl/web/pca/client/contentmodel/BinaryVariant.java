package com.sdl.web.pca.client.contentmodel;


/**
*Represents a variant of a binary component.
*/
public class BinaryVariant{
		private int binaryId;
		private String description;
		private String downloadUrl;
		private String id;
		private String path;
		private String type;
		private String url;
		private String variantId;


		public int getBinaryId()
		{
			return binaryId;
		}
		public void setBinaryId(int binaryId)
		{
			this.binaryId = binaryId;
		}


		public String getDescription()
		{
			return description;
		}
		public void setDescription(String description)
		{
			this.description = description;
		}


		public String getDownloadUrl()
		{
			return downloadUrl;
		}
		public void setDownloadUrl(String downloadUrl)
		{
			this.downloadUrl = downloadUrl;
		}


		public String getId()
		{
			return id;
		}
		public void setId(String id)
		{
			this.id = id;
		}


		public String getPath()
		{
			return path;
		}
		public void setPath(String path)
		{
			this.path = path;
		}


		public String getType()
		{
			return type;
		}
		public void setType(String type)
		{
			this.type = type;
		}


		public String getUrl()
		{
			return url;
		}
		public void setUrl(String url)
		{
			this.url = url;
		}


		public String getVariantId()
		{
			return variantId;
		}
		public void setVariantId(String variantId)
		{
			this.variantId = variantId;
		}
	
}
