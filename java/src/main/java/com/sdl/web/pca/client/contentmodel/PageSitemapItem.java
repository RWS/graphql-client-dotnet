package com.sdl.web.pca.client.contentmodel;

class PageSitemapItem implements ISitemapItem
	{
		private String id;
		private String originalTitle;
		private String publishedDate;
		private String title;
		private String type;
		private String url;
		private boolean visible;

		 public String getId()
		 {
			 return id;
		 }
		 public void setId(String id)
		 {
			 this.id = id;
		 }

		 public String getOriginalTitle()
		 {
			 return originalTitle;
		 }
		 public void setOriginalTitle(String originalTitle)
		 {
			 this.originalTitle = originalTitle;
		 }

		 public String getPublishedDate()
		 {
			 return publishedDate;
		 }
		 public void setPublishedDate(String publishedDate)
		 {
			 this.publishedDate = publishedDate;
		 }

		 public String getTitle()
		 {
			 return title;
		 }
		 public void setTitle(String title)
		 {
			 this.title = title;
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

		 public boolean isVisible()
		 {
			 return visible;
		 }
		 public void setVisible(boolean visible)
		 {
			 this.visible = visible;
		 }
	}
