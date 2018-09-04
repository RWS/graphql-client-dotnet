package com.sdl.web.pca.client.contentmodel;


/**
*Represents a Link.
*/
public class Link {
		private int itemId;
		private int namespaceId;
		private int publicationId;
		private LinkType type;
		private String url;


		public int getItemId(){
			return itemId;
		}
		public void setItemId(int itemId){
			this.itemId = itemId;
		}


		public int getNamespaceId(){
			return namespaceId;
		}
		public void setNamespaceId(int namespaceId){
			this.namespaceId = namespaceId;
		}


		public int getPublicationId(){
			return publicationId;
		}
		public void setPublicationId(int publicationId){
			this.publicationId = publicationId;
		}


		public LinkType getType(){
			return type;
		}
		public void setType(LinkType type){
			this.type = type;
		}


		public String getUrl(){
			return url;
		}
		public void setUrl(String url){
			this.url = url;
		}
	
}
