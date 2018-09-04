package com.sdl.web.pca.client.contentmodel.generated;

import java.util.Map;

/**
*Untyped content where all data is available in a Map structure.
*/
public class UntypedContent implements Content {
		private Map data;
		private String id;
		private String type;


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


		public String getType(){
			return type;
		}
		public void setType(String type){
			this.type = type;
		}
	
}
