package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// Represents a template.
	/// </summary>
	class Template implements IItem
	{
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private int itemType;
		private String lastPublishDate;
		private int namespaceId;
		private String outputFormat;
		private int owningPublicationId;
		private int priority;
		private int publicationId;
		private String title;
		private String updatedDate;

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

		 public String getOutputFormat()
		 {
			 return outputFormat;
		 }
		 public void setOutputFormat(String outputFormat)
		 {
			 this.outputFormat = outputFormat;
		 }

		 public int getOwningPublicationId()
		 {
			 return owningPublicationId;
		 }
		 public void setOwningPublicationId(int owningPublicationId)
		 {
			 this.owningPublicationId = owningPublicationId;
		 }

		 public int getPriority()
		 {
			 return priority;
		 }
		 public void setPriority(int priority)
		 {
			 this.priority = priority;
		 }

		 public int getPublicationId()
		 {
			 return publicationId;
		 }
		 public void setPublicationId(int publicationId)
		 {
			 this.publicationId = publicationId;
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
	}
