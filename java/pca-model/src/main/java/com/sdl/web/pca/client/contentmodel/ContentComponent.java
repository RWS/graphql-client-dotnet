package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;
import java.util.List;

/**
*Represents a component which has content.
*/
public interface ContentComponent{
																														

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

		ItemType getItemType();
		void setItemType(ItemType itemType);

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
