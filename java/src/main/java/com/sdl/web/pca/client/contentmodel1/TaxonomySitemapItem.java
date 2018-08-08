package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

class TaxonomySitemapItem implements ISitemapItem
	{
		private int classifiedItemsCount;
		private String description;
		private String id;
		private List<ISitemapItem> items;
		private String key;
		private String originalTitle;
		private String publishedDate;
		private String title;
		private String type;
		private String url;
		private boolean Abstract;
		private boolean hasChildNodes;
		private boolean visible;

		 public int getClassifiedItemsCount()
		 {
			 return classifiedItemsCount;
		 }
		 public void setClassifiedItemsCount(int classifiedItemsCount)
		 {
			 this.classifiedItemsCount = classifiedItemsCount;
		 }

		 public String getDescription()
		 {
			 return description;
		 }
		 public void setDescription(String description)
		 {
			 this.description = description;
		 }

		 public String getId()
		 {
			 return id;
		 }
		 public void setId(String id)
		 {
			 this.id = id;
		 }

		 public List<ISitemapItem> getItems()
		 {
			 return items;
		 }
		 public void setItems(List<ISitemapItem> items)
		 {
			 this.items = items;
		 }

		 public String getKey()
		 {
			 return key;
		 }
		 public void setKey(String key)
		 {
			 this.key = key;
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

		 public boolean isAbstract()
		 {
			 return Abstract;
		 }
		 public void setAbstract(boolean anAbstract)
		 {
			 this.Abstract = anAbstract;
		 }

		 public boolean isHasChildNodes()
		 {
			 return hasChildNodes;
		 }
		 public void setHasChildNodes(boolean hasChildNodes)
		 {
			 this.hasChildNodes = hasChildNodes;
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
