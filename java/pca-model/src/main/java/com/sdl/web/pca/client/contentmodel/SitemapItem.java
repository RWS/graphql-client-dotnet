package com.sdl.web.pca.client.contentmodel;


/**
*Sitemap Item
*/
public interface SitemapItem{
		String id = null;
		String originalTitle = null;
		String publishedDate = null;
		String title = null;
		String type = null;
		String url = null;
		boolean visible = false;


		String getId();
		void setId(String id);

		String getOriginalTitle();
		void setOriginalTitle(String originalTitle);

		String getPublishedDate();
		void setPublishedDate(String publishedDate);

		String getTitle();
		void setTitle(String title);

		String getType();
		void setType(String type);

		String getUrl();
		void setUrl(String url);

		boolean getVisible();
		void setVisible(boolean visible);	
}
