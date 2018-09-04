package com.sdl.web.pca.client.contentmodel;


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

		int getNamespaceId();
		void setNamespaceId(int namespaceId);

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
