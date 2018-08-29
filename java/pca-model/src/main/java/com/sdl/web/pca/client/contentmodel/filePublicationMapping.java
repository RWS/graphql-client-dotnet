package com.sdl.web.pca.client.contentmodel;


/**
*Represents a publication mapping.
*/
public class PublicationMapping{
		private String domain;
		private ContentNamespace namespaceId;
		private String path;
		private int pathScanDepth;
		private String port;
		private String protocol;
		private int publicationId;


		public String getDomain()
		{
			return domain;
		}
		public void setDomain(String domain)
		{
			this.domain = domain;
		}


		public ContentNamespace getNamespaceId()
		{
			return namespaceId;
		}
		public void setNamespaceId(ContentNamespace namespaceId)
		{
			this.namespaceId = namespaceId;
		}


		public String getPath()
		{
			return path;
		}
		public void setPath(String path)
		{
			this.path = path;
		}


		public int getPathScanDepth()
		{
			return pathScanDepth;
		}
		public void setPathScanDepth(int pathScanDepth)
		{
			this.pathScanDepth = pathScanDepth;
		}


		public String getPort()
		{
			return port;
		}
		public void setPort(String port)
		{
			this.port = port;
		}


		public String getProtocol()
		{
			return protocol;
		}
		public void setProtocol(String protocol)
		{
			this.protocol = protocol;
		}


		public int getPublicationId()
		{
			return publicationId;
		}
		public void setPublicationId(int publicationId)
		{
			this.publicationId = publicationId;
		}
	
}
