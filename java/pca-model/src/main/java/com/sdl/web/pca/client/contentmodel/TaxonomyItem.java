package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;

/**
*Represents a taxonomy item.
*/
public interface TaxonomyItem{
		TaxonomyItemConnection children = null;
		String creationDate = null;
		CustomMetaConnection customMetas = null;
		int depth = 0;
		String id = null;
		String initialPublishDate = null;
		int itemId = 0;
		ItemType itemType = null;
		String key = null;
		String lastPublishDate = null;
		ContentNamespace namespaceId = null;
		int owningPublicationId = 0;
		TaxonomyItem parent = null;
		int publicationId = 0;
		int taxonomyId = 0;
		TaxonomyType taxonomyType = null;
		String title = null;
		String updatedDate = null;
		boolean hasChildren = false;
		boolean navigable = false;


		TaxonomyItemConnection getChildren();
		void setChildren(TaxonomyItemConnection children);

		String getCreationDate();
		void setCreationDate(String creationDate);

		CustomMetaConnection getCustomMetas();
		void setCustomMetas(CustomMetaConnection customMetas);

		int getDepth();
		void setDepth(int depth);

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

		ContentNamespace getNamespaceId();
		void setNamespaceId(ContentNamespace namespaceId);

		int getOwningPublicationId();
		void setOwningPublicationId(int owningPublicationId);

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
