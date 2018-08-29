package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*Represents a page.
*/
public class Page implements Container,Taggable,Item{
		private List<Item> containerItems;
		private Content content;
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String fileName;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private com.sdl.web.pca.client.contentmodel.enums.ItemType itemType;
		private String lastPublishDate;
		private ContentNamespace namespaceId;
		private int owningPublicationId;
		private Template pageTemplate;
		private int publicationId;
		private RawContent rawContent;
		private List<TaxonomyItem> taxonomies;
		private String title;
		private String updatedDate;
		private String url;


		public List<Item> getContainerItems()
		{
			return containerItems;
		}
		public void setContainerItems(List<Item> containerItems)
		{
			this.containerItems = containerItems;
		}


		public Content getContent()
		{
			return content;
		}
		public void setContent(Content content)
		{
			this.content = content;
		}


		public String getCreationDate()
		{
			return creationDate;
		}
		public void setCreationDate(String creationDate)
		{
			this.creationDate = creationDate;
		}


		public CustomMetaConnection getCustomMetas()
		{
			return customMetas;
		}
		public void setCustomMetas(CustomMetaConnection customMetas)
		{
			this.customMetas = customMetas;
		}


		public String getFileName()
		{
			return fileName;
		}
		public void setFileName(String fileName)
		{
			this.fileName = fileName;
		}


		public String getId()
		{
			return id;
		}
		public void setId(String id)
		{
			this.id = id;
		}


		public String getInitialPublishDate()
		{
			return initialPublishDate;
		}
		public void setInitialPublishDate(String initialPublishDate)
		{
			this.initialPublishDate = initialPublishDate;
		}


		public int getItemId()
		{
			return itemId;
		}
		public void setItemId(int itemId)
		{
			this.itemId = itemId;
		}


		public com.sdl.web.pca.client.contentmodel.enums.ItemType getItemType()
		{
			return itemType;
		}
		public void setItemType(com.sdl.web.pca.client.contentmodel.enums.ItemType itemType)
		{
			this.itemType = itemType;
		}


		public String getLastPublishDate()
		{
			return lastPublishDate;
		}
		public void setLastPublishDate(String lastPublishDate)
		{
			this.lastPublishDate = lastPublishDate;
		}


		public ContentNamespace getNamespaceId()
		{
			return namespaceId;
		}
		public void setNamespaceId(ContentNamespace namespaceId)
		{
			this.namespaceId = namespaceId;
		}


		public int getOwningPublicationId()
		{
			return owningPublicationId;
		}
		public void setOwningPublicationId(int owningPublicationId)
		{
			this.owningPublicationId = owningPublicationId;
		}


		public Template getPageTemplate()
		{
			return pageTemplate;
		}
		public void setPageTemplate(Template pageTemplate)
		{
			this.pageTemplate = pageTemplate;
		}


		public int getPublicationId()
		{
			return publicationId;
		}
		public void setPublicationId(int publicationId)
		{
			this.publicationId = publicationId;
		}


		public RawContent getRawContent()
		{
			return rawContent;
		}
		public void setRawContent(RawContent rawContent)
		{
			this.rawContent = rawContent;
		}


		public List<TaxonomyItem> getTaxonomies()
		{
			return taxonomies;
		}
		public void setTaxonomies(List<TaxonomyItem> taxonomies)
		{
			this.taxonomies = taxonomies;
		}


		public String getTitle()
		{
			return title;
		}
		public void setTitle(String title)
		{
			this.title = title;
		}


		public String getUpdatedDate()
		{
			return updatedDate;
		}
		public void setUpdatedDate(String updatedDate)
		{
			this.updatedDate = updatedDate;
		}


		public String getUrl()
		{
			return url;
		}
		public void setUrl(String url)
		{
			this.url = url;
		}
	
}
