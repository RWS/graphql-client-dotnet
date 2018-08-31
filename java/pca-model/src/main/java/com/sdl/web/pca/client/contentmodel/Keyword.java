package com.sdl.web.pca.client.contentmodel;

import com.sdl.web.pca.client.contentmodel.enums.ItemType;

/**
*Represents a keyword taxonomy item.
*/
public class Keyword implements TaxonomyItem,Item {
		private TaxonomyItemConnection children;
		private String creationDate;
		private CustomMetaConnection customMetas;
		private Integer depth;
		private String description;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private ItemType itemType;
		private String key;
		private String lastPublishDate;
		private String name;
		private Integer namespaceId;
		private Integer owningPublicationId;
		private TaxonomyItem parent;
		private int publicationId;
		private int taxonomyId;
		private TaxonomyType taxonomyType;
		private String title;
		private Integer totalRelatedItems;
		private String updatedDate;
		private boolean hasChildren;
		private boolean navigable;
		private boolean usedForIdentification;


		public TaxonomyItemConnection getChildren(){
			return children;
		}
		public void setChildren(TaxonomyItemConnection children){
			this.children = children;
		}


		public String getCreationDate(){
			return creationDate;
		}
		public void setCreationDate(String creationDate){
			this.creationDate = creationDate;
		}


		public CustomMetaConnection getCustomMetas(){
			return customMetas;
		}
		public void setCustomMetas(CustomMetaConnection customMetas){
			this.customMetas = customMetas;
		}


		public Integer getDepth(){
			return depth;
		}
		public void setDepth(Integer depth){
			this.depth = depth;
		}


		public String getDescription(){
			return description;
		}
		public void setDescription(String description){
			this.description = description;
		}


		public String getId(){
			return id;
		}
		public void setId(String id){
			this.id = id;
		}


		public String getInitialPublishDate(){
			return initialPublishDate;
		}
		public void setInitialPublishDate(String initialPublishDate){
			this.initialPublishDate = initialPublishDate;
		}


		public int getItemId(){
			return itemId;
		}
		public void setItemId(int itemId){
			this.itemId = itemId;
		}


		public ItemType getItemType(){
			return itemType;
		}
		public void setItemType(ItemType itemType){
			this.itemType = itemType;
		}


		public String getKey(){
			return key;
		}
		public void setKey(String key){
			this.key = key;
		}


		public String getLastPublishDate(){
			return lastPublishDate;
		}
		public void setLastPublishDate(String lastPublishDate){
			this.lastPublishDate = lastPublishDate;
		}


		public String getName(){
			return name;
		}
		public void setName(String name){
			this.name = name;
		}


		public Integer getNamespaceId(){
			return namespaceId;
		}
		public void setNamespaceId(Integer namespaceId){
			this.namespaceId = namespaceId;
		}


		public Integer getOwningPublicationId(){
			return owningPublicationId;
		}
		public void setOwningPublicationId(Integer owningPublicationId){
			this.owningPublicationId = owningPublicationId;
		}


		public TaxonomyItem getParent(){
			return parent;
		}
		public void setParent(TaxonomyItem parent){
			this.parent = parent;
		}


		public int getPublicationId(){
			return publicationId;
		}
		public void setPublicationId(int publicationId){
			this.publicationId = publicationId;
		}


		public int getTaxonomyId(){
			return taxonomyId;
		}
		public void setTaxonomyId(int taxonomyId){
			this.taxonomyId = taxonomyId;
		}


		public TaxonomyType getTaxonomyType(){
			return taxonomyType;
		}
		public void setTaxonomyType(TaxonomyType taxonomyType){
			this.taxonomyType = taxonomyType;
		}


		public String getTitle(){
			return title;
		}
		public void setTitle(String title){
			this.title = title;
		}


		public Integer getTotalRelatedItems(){
			return totalRelatedItems;
		}
		public void setTotalRelatedItems(Integer totalRelatedItems){
			this.totalRelatedItems = totalRelatedItems;
		}


		public String getUpdatedDate(){
			return updatedDate;
		}
		public void setUpdatedDate(String updatedDate){
			this.updatedDate = updatedDate;
		}


		public boolean getHasChildren(){
			return hasChildren;
		}
		public void setHasChildren(boolean hasChildren){
			this.hasChildren = hasChildren;
		}


		public boolean getNavigable(){
			return navigable;
		}
		public void setNavigable(boolean navigable){
			this.navigable = navigable;
		}


		public boolean getUsedForIdentification(){
			return usedForIdentification;
		}
		public void setUsedForIdentification(boolean usedForIdentification){
			this.usedForIdentification = usedForIdentification;
		}
	
}
