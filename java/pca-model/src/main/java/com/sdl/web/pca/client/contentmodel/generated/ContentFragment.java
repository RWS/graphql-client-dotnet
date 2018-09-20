package com.sdl.web.pca.client.contentmodel.generated;


/**
*Represents a fragment of content.
*/
public interface ContentFragment {
																										

		Content getContent();
		void setContent(Content content);

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
