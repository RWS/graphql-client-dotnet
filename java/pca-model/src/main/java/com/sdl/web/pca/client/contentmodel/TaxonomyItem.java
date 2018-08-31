package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;

/**
*Represents a taxonomy item.
*/
public interface TaxonomyItem {
																																								

		TaxonomyItemConnection getChildren();
		void setChildren(TaxonomyItemConnection children);

		String getCreationDate();
		void setCreationDate(String creationDate);

		CustomMetaConnection getCustomMetas();
		void setCustomMetas(CustomMetaConnection customMetas);

		Integer getDepth();
		void setDepth(Integer depth);

		String getId();
		void setId(String id);

		String getInitialPublishDate();
		void setInitialPublishDate(String initialPublishDate);

		int getItemId();
		void setItemId(int itemId);

		ItemType getItemType();
		void setItemType(ItemType itemType);

		String getKey();
		void setKey(String key);

		String getLastPublishDate();
		void setLastPublishDate(String lastPublishDate);

		Integer getNamespaceId();
		void setNamespaceId(Integer namespaceId);

		Integer getOwningPublicationId();
		void setOwningPublicationId(Integer owningPublicationId);

		TaxonomyItem getParent();
		void setParent(TaxonomyItem parent);

		int getPublicationId();
		void setPublicationId(int publicationId);

		int getTaxonomyId();
		void setTaxonomyId(int taxonomyId);

		TaxonomyType getTaxonomyType();
		void setTaxonomyType(TaxonomyType taxonomyType);

		String getTitle();
		void setTitle(String title);

		String getUpdatedDate();
		void setUpdatedDate(String updatedDate);

		boolean getHasChildren();
		void setHasChildren(boolean hasChildren);

		boolean getNavigable();
		void setNavigable(boolean navigable);	
}
