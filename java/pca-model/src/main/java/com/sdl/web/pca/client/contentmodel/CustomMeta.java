package com.sdl.web.pca.client.contentmodel;


/**
*Represents custom metadata.
*/
public class CustomMeta{
		private String id;
		private int itemId;
		private String key;
		private Integer namespaceId;
		private int publicationId;
		private String value;
		private CustomMetaValueType valueType;


		public String getId()
		{
			return id;
		}
		public void setId(String id)
		{
			this.id = id;
		}


		public int getItemId()
		{
			return itemId;
		}
		public void setItemId(int itemId)
		{
			this.itemId = itemId;
		}


		public String getKey()
		{
			return key;
		}
		public void setKey(String key)
		{
			this.key = key;
		}


		public Integer getNamespaceId()
		{
			return namespaceId;
		}
		public void setNamespaceId(Integer namespaceId)
		{
			this.namespaceId = namespaceId;
		}


		public int getPublicationId()
		{
			return publicationId;
		}
		public void setPublicationId(int publicationId)
		{
			this.publicationId = publicationId;
		}


		public String getValue()
		{
			return value;
		}
		public void setValue(String value)
		{
			this.value = value;
		}


		public CustomMetaValueType getValueType()
		{
			return valueType;
		}
		public void setValueType(CustomMetaValueType valueType)
		{
			this.valueType = valueType;
		}
	
}
