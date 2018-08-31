package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;

/**
*Represents a publication.
*/
public class Publication implements Item {
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private ItemType itemType;
		private String lastPublishDate;
		private String multimediaPath;
		private String multimediaUrl;
		private Integer namespaceId;
		private Integer owningPublicationId;
		private int publicationId;
		private String publicationKey;
		private String publicationPath;
		private String publicationUrl;
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


		public ItemType getItemType()
		{
			return itemType;
		}
		public void setItemType(ItemType itemType)
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


		public String getMultimediaPath()
		{
			return multimediaPath;
		}
		public void setMultimediaPath(String multimediaPath)
		{
			this.multimediaPath = multimediaPath;
		}


		public String getMultimediaUrl()
		{
			return multimediaUrl;
		}
		public void setMultimediaUrl(String multimediaUrl)
		{
			this.multimediaUrl = multimediaUrl;
		}


		public Integer getNamespaceId()
		{
			return namespaceId;
		}
		public void setNamespaceId(Integer namespaceId)
		{
			this.namespaceId = namespaceId;
		}


		public Integer getOwningPublicationId()
		{
			return owningPublicationId;
		}
		public void setOwningPublicationId(Integer owningPublicationId)
		{
			this.owningPublicationId = owningPublicationId;
		}


		public int getPublicationId()
		{
			return publicationId;
		}
		public void setPublicationId(int publicationId)
		{
			this.publicationId = publicationId;
		}


		public String getPublicationKey()
		{
			return publicationKey;
		}
		public void setPublicationKey(String publicationKey)
		{
			this.publicationKey = publicationKey;
		}


		public String getPublicationPath()
		{
			return publicationPath;
		}
		public void setPublicationPath(String publicationPath)
		{
			this.publicationPath = publicationPath;
		}


		public String getPublicationUrl()
		{
			return publicationUrl;
		}
		public void setPublicationUrl(String publicationUrl)
		{
			this.publicationUrl = publicationUrl;
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
