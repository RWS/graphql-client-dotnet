package com.sdl.web.pca.client.contentmodel1;

import java.util.List;

/// <summary>
	/// Represents a page.
	/// </summary>
	class Page implements IContainer, IItem
	{
		private List<IItem> containerItems;
		private IContent content;
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String fileName;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private int itemType;
		private String lastPublishDate;
		private int namespaceId;
		private int owningPublicationId;
		private Template pageTemplate;
		private int publicationId;
		private RawContent rawContent;
		private List<ITaxonomyItem> taxonomies;
		private String title;
		private String updatedDate;
		private String url;

		 public List<IItem> getContainerItems()
		 {
			 return containerItems;
		 }
		 public void setContainerItems(List<IItem> containerItems)
		 {
			 this.containerItems = containerItems;
		 }

		 public IContent getContent()
		 {
			 return content;
		 }
		 public void setContent(IContent content)
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

		 public int getItemType()
		 {
			 return itemType;
		 }
		 public void setItemType(int itemType)
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

		 public int getNamespaceId()
		 {
			 return namespaceId;
		 }
		 public void setNamespaceId(int namespaceId)
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

		 public List<ITaxonomyItem> getTaxonomies()
		 {
			 return taxonomies;
		 }
		 public void setTaxonomies(List<ITaxonomyItem> taxonomies)
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
