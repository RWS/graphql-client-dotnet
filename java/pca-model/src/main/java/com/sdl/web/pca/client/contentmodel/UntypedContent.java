package com.sdl.web.pca.client.contentmodel;

import java.util.Dictionary;

/**
*Untyped content where all data is available in a Map structure.
*/
public class UntypedContent implements Content {
		private Dictionary data;
		private String id;
		private String type;


		public Dictionary getData()
		{
			return data;
		}
		public void setData(Dictionary data)
		{
			this.data = data;
		}


		public String getId()
		{
			return id;
		}
		public void setId(String id)
		{
			this.id = id;
		}


		public String getType()
		{
			return type;
		}
		public void setType(String type)
		{
			this.type = type;
		}
	
}
