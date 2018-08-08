package com.sdl.web.pca.client.contentmodel;

/// <summary>
	/// Represents a taxonomy item.
	/// </summary>
	interface ITaxonomyItem
	{
		TaxonomyItemConnection children = null;
		String creationDate = null;
		CustomMetaConnection customMetas = null;
		int depth = 0;
		String id = null;
		String initialPublishDate = null;
		int itemId = 0;
		int itemType = 0;
		String key = null;
		String lastPublishDate = null;
		int namespaceId = 0;
		int owningPublicationId = 0;
		ITaxonomyItem parent = null;
		int publicationId = 0;
		int taxonomyId = 0;
		TaxonomyType taxonomyType = null;
		String title = null;
		String updatedDate = null;
		boolean hasChildren = false;
		boolean Abstract = false;
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

		int getItemType();
		void setItemType(int itemType);

		String getKey();
		void setKey(String key);

		String getLastPublishDate();
		void setLastPublishDate(String lastPublishDate);

		int getNamespaceId();
		void setNamespaceId(int namespaceId);

		int getOwningPublicationId();
		void setOwningPublicationId(int owningPublicationId);

		ITaxonomyItem getParent();
		void setParent(ITaxonomyItem parent);

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

		boolean isHasChildren();
		void setHasChildren(boolean hasChildren);

		boolean isAbstract();
		void setAbstract(boolean anAbstract);

		boolean isNavigable();
		void setNavigable(boolean navigable);
	}
