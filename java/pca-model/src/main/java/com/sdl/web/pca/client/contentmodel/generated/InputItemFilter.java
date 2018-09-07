package com.sdl.web.pca.client.contentmodel.generated;

import java.util.List;

/**
*Item filter for the items root query. When the filter is applied, only the items matching the filter will be in the query result
*/
public class InputItemFilter {
		private List<InputItemFilter> and;
		private InputCustomMetaCriteria customMeta;
		private List<ItemType> itemTypes;
		private InputKeywordCriteria keyword;
		private List<Integer> namespaceIds;
		private List<InputItemFilter> or;
		private List<Integer> publicationIds;
		private InputSchemaCriteria schema;


		public List<InputItemFilter> getAnd(){
			return and;
		}
		public void setAnd(List<InputItemFilter> and){
			this.and = and;
		}


		public InputCustomMetaCriteria getCustomMeta(){
			return customMeta;
		}
		public void setCustomMeta(InputCustomMetaCriteria customMeta){
			this.customMeta = customMeta;
		}


		public List<ItemType> getItemTypes(){
			return itemTypes;
		}
		public void setItemTypes(List<ItemType> itemTypes){
			this.itemTypes = itemTypes;
		}


		public InputKeywordCriteria getKeyword(){
			return keyword;
		}
		public void setKeyword(InputKeywordCriteria keyword){
			this.keyword = keyword;
		}


		public List<Integer> getNamespaceIds(){
			return namespaceIds;
		}
		public void setNamespaceIds(List<Integer> namespaceIds){
			this.namespaceIds = namespaceIds;
		}


		public List<InputItemFilter> getOr(){
			return or;
		}
		public void setOr(List<InputItemFilter> or){
			this.or = or;
		}


		public List<Integer> getPublicationIds(){
			return publicationIds;
		}
		public void setPublicationIds(List<Integer> publicationIds){
			this.publicationIds = publicationIds;
		}


		public InputSchemaCriteria getSchema(){
			return schema;
		}
		public void setSchema(InputSchemaCriteria schema){
			this.schema = schema;
		}
	
}
