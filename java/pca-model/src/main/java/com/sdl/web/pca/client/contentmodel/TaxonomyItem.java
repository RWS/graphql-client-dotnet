package com.sdl.web.pca.client.contentmodel;

import java.util.Map;import java.util.Map;import java.util.Map;/**
*Represents a taxonomy item.
*/
public class TaxonomyItem{

		public TaxonomyItemConnection children;

		public String creationDate;

		public CustomMetaConnection customMetas;

		public int depth;


		public String initialPublishDate;


		public ItemType itemType;

		public String key;

		public String lastPublishDate;

		public ContentNamespace namespaceId;

		public int owningPublicationId;

		public TaxonomyItem parent;




		public String title;

		public String updatedDate;

		public Boolean hasChildren;


		public Boolean navigable;
	
}
