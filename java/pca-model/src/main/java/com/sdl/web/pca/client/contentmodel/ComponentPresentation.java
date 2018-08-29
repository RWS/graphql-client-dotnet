package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;

/**
*Represents a component presentation which has a component associated with a template.
*/
public class ComponentPresentation implements ContentFragment,Item{
		private ContentComponent component;
		private Template componentTemplate;
		private Content content;
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private ItemType itemType;
		private String lastPublishDate;
		private ContentNamespace namespaceId;
		private int owningPublicationId;
		private int publicationId;
		private RawContent rawContent;
		private String title;
		private String updatedDate;


		public ContentComponent getComponent()
		{
			return component;
		}
		public void setComponent(ContentComponent component)
		{
			this.component = component;
		}


		public Template getComponentTemplate()
		{
			return componentTemplate;
		}
		public void setComponentTemplate(Template componentTemplate)
		{
			this.componentTemplate = componentTemplate;
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
