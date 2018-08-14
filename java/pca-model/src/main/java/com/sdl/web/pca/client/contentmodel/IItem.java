package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// Represents an item. The root of all content models.
	/// </summary>
	public interface IItem
	{
		String creationDate = null;
		CustomMetaConnection customMetas = null;
		String id = null;
		String initialPublishDate = null;
		int itemId = 0;
		int itemType = 0;
		String lastPublishDate = null;
		int namespaceId = 0;
		int owningPublicationId = 0;
		int publicationId = 0;
		String title = null;
		String updatedDate = null;


		String getCreationDate();
		void setCreationDate(String creationDate);

		CustomMetaConnection getCustomMetas();
		void setCustomMetas(CustomMetaConnection customMetas);

		String getId();
		void setId(String id);

		String getInitialPublishDate();
		void setInitialPublishDate(String initialPublishDate);

		int getItemId();
		void setItemId(int itemId);

		int getItemType();
		void setItemType(int itemType);

		String getLastPublishDate();
		void setLastPublishDate(String lastPublishDate);

		int getNamespaceId();
		void setNamespaceId(int namespaceId);

		int getOwningPublicationId();
		void setOwningPublicationId(int owningPublicationId);

		int getPublicationId();
		void setPublicationId(int publicationId);

		String getTitle();
		void setTitle(String title);

		String getUpdatedDate();
		void setUpdatedDate(String updatedDate);
	}
