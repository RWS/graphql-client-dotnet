package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*Represents a component which has content.
*/
public interface ContentComponent{
		String creationDate = null;
		CustomMetaConnection customMetas = null;
		String id = null;
		String initialPublishDate = null;
		int itemId = 0;
		com.sdl.web.pca.client.contentmodel.enums.ItemType itemType = null;
		String lastPublishDate = null;
		ContentNamespace namespaceId = null;
		int owningPublicationId = 0;
		int publicationId = 0;
		int schemaId = 0;
		List<TaxonomyItem> taxonomies = null;
		String title = null;
		String updatedDate = null;
		boolean multiMedia = false;


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

		com.sdl.web.pca.client.contentmodel.enums.ItemType getItemType();
		void setItemType(com.sdl.web.pca.client.contentmodel.enums.ItemType itemType);

		String getLastPublishDate();
		void setLastPublishDate(String lastPublishDate);

		ContentNamespace getNamespaceId();
		void setNamespaceId(ContentNamespace namespaceId);

		int getOwningPublicationId();
		void setOwningPublicationId(int owningPublicationId);

		int getPublicationId();
		void setPublicationId(int publicationId);

		int getSchemaId();
		void setSchemaId(int schemaId);

		List<TaxonomyItem> getTaxonomies();
		void setTaxonomies(List<TaxonomyItem> taxonomies);

		String getTitle();
		void setTitle(String title);

		String getUpdatedDate();
		void setUpdatedDate(String updatedDate);

		boolean getMultiMedia();
		void setMultiMedia(boolean multiMedia);	
}
