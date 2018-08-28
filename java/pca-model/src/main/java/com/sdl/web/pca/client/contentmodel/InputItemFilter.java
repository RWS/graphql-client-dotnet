package com.sdl.web.pca.client.contentmodel;

import java.util.List;

/**
*Item filter
*/
public class InputItemFilter{
		private List<InputItemFilter> and;
		private InputCustomMetaCriteria customMeta;
		private List<ItemType> itemTypes;
		private InputKeywordCriteria keyword;
		private List<ContentNamespace> namespaceIds;
		private List<InputItemFilter> or;
		private List<Integer> publicationIds;


		public List<InputItemFilter> getAnd()
		{
			return and;
		}
		public void setAnd(List<InputItemFilter> and)
		{
			this.and = and;
		}


		public InputCustomMetaCriteria getCustomMeta()
		{
			return customMeta;
		}
		public void setCustomMeta(InputCustomMetaCriteria customMeta)
		{
			this.customMeta = customMeta;
		}


		public List<ItemType> getItemTypes()
		{
			return itemTypes;
		}
		public void setItemTypes(List<ItemType> itemTypes)
		{
			this.itemTypes = itemTypes;
		}


		public InputKeywordCriteria getKeyword()
		{
			return keyword;
		}
		public void setKeyword(InputKeywordCriteria keyword)
		{
			this.keyword = keyword;
		}


		public List<ContentNamespace> getNamespaceIds()
		{
			return namespaceIds;
		}
		public void setNamespaceIds(List<ContentNamespace> namespaceIds)
		{
			this.namespaceIds = namespaceIds;
		}


		public List<InputItemFilter> getOr()
		{
			return or;
		}
		public void setOr(List<InputItemFilter> or)
		{
			this.or = or;
		}


		public List<Integer> getPublicationIds()
		{
			return publicationIds;
		}
		public void setPublicationIds(List<Integer> publicationIds)
		{
			this.publicationIds = publicationIds;
		}
	
}
