package com.sdl.web.pca.client.contentmodel;

import java.util.Map;

/**
*Represents an item with content.
*/
public class RawContent {
		private String charSet;
		private String content;
		private Map data;
		private String id;


		public String getCharSet(){
			return charSet;
		}
		public void setCharSet(String charSet){
			this.charSet = charSet;
		}


		public String getContent(){
			return content;
		}
		public void setContent(String content){
			this.content = content;
		}


		public Map getData(){
			return data;
		}
		public void setData(Map data){
			this.data = data;
		}


		public String getId(){
			return id;
		}
		public void setId(String id){
			this.id = id;
		}
	
}
