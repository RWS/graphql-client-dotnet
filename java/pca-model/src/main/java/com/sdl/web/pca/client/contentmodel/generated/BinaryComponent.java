package com.sdl.web.pca.client.contentmodel.generated;

import java.util.List;

/**
*Represents a component which has binary content.
*/
public class BinaryComponent implements ContentComponent,Item {
		private String creationDate;
		private CustomMetaConnection customMetas;
		private String id;
		private String initialPublishDate;
		private int itemId;
		private int itemType;
		private String lastPublishDate;
		private int namespaceId;
		private int owningPublicationId;
		private int publicationId;
		private int schemaId;
		private List<TaxonomyItem> taxonomies;
		private String title;
		private String type;
		private String updatedDate;
		private BinaryVariantConnection variants;
		private boolean multiMedia;


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


		public int getItemType(){
			return itemType;
		}
		public void setItemType(int itemType){
			this.itemType = itemType;
		}


		public String getLastPublishDate(){
			return lastPublishDate;
		}
		public void setLastPublishDate(String lastPublishDate){
			this.lastPublishDate = lastPublishDate;
		}


		public int getNamespaceId(){
			return namespaceId;
		}
		public void setNamespaceId(int namespaceId){
			this.namespaceId = namespaceId;
		}


		public int getOwningPublicationId(){
			return owningPublicationId;
		}
		public void setOwningPublicationId(int owningPublicationId){
			this.owningPublicationId = owningPublicationId;
		}


		public int getPublicationId(){
			return publicationId;
		}
		public void setPublicationId(int publicationId){
			this.publicationId = publicationId;
		}


		public int getSchemaId(){
			return schemaId;
		}
		public void setSchemaId(int schemaId){
			this.schemaId = schemaId;
		}


		public List<TaxonomyItem> getTaxonomies(){
			return taxonomies;
		}
		public void setTaxonomies(List<TaxonomyItem> taxonomies){
			this.taxonomies = taxonomies;
		}


		public String getTitle(){
			return title;
		}
		public void setTitle(String title){
			this.title = title;
		}


		public String getType(){
			return type;
		}
		public void setType(String type){
			this.type = type;
		}


		public String getUpdatedDate(){
			return updatedDate;
		}
		public void setUpdatedDate(String updatedDate){
			this.updatedDate = updatedDate;
		}


		public BinaryVariantConnection getVariants(){
			return variants;
		}
		public void setVariants(BinaryVariantConnection variants){
			this.variants = variants;
		}


		public boolean getMultiMedia(){
			return multiMedia;
		}
		public void setMultiMedia(boolean multiMedia){
			this.multiMedia = multiMedia;
		}
	
}
