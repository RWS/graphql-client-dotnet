package com.sdl.web.pca.client.contentmodel;


/**
*The query root for the GraphQL Public Content API.
*/
public class ContentQuery {
		private BinaryComponent binaryComponent;
		private Link binaryLink;
		private KeywordConnection categories;
		private Link componentLink;
		private ComponentPresentation componentPresentation;
		private Link dynamicComponentLink;
		private ItemConnection items;
		private Keyword keyword;
		private Page page;
		private Link pageLink;
		private PageConnection pages;
		private Publication publication;
		private PublicationMapping publicationMapping;
		private PublicationConnection publications;
		private StructureGroup structureGroup;
		private StructureGroupConnection structureGroups;
		private TaxonomySitemapItem sitemap;
		private TaxonomySitemapItem sitemapSubtree;


		public BinaryComponent getBinaryComponent(){
			return binaryComponent;
		}
		public void setBinaryComponent(BinaryComponent binaryComponent){
			this.binaryComponent = binaryComponent;
		}


		public Link getBinaryLink(){
			return binaryLink;
		}
		public void setBinaryLink(Link binaryLink){
			this.binaryLink = binaryLink;
		}


		public KeywordConnection getCategories(){
			return categories;
		}
		public void setCategories(KeywordConnection categories){
			this.categories = categories;
		}


		public Link getComponentLink(){
			return componentLink;
		}
		public void setComponentLink(Link componentLink){
			this.componentLink = componentLink;
		}


		public ComponentPresentation getComponentPresentation(){
			return componentPresentation;
		}
		public void setComponentPresentation(ComponentPresentation componentPresentation){
			this.componentPresentation = componentPresentation;
		}


		public Link getDynamicComponentLink(){
			return dynamicComponentLink;
		}
		public void setDynamicComponentLink(Link dynamicComponentLink){
			this.dynamicComponentLink = dynamicComponentLink;
		}


		public ItemConnection getItems(){
			return items;
		}
		public void setItems(ItemConnection items){
			this.items = items;
		}


		public Keyword getKeyword(){
			return keyword;
		}
		public void setKeyword(Keyword keyword){
			this.keyword = keyword;
		}


		public Page getPage(){
			return page;
		}
		public void setPage(Page page){
			this.page = page;
		}


		public Link getPageLink(){
			return pageLink;
		}
		public void setPageLink(Link pageLink){
			this.pageLink = pageLink;
		}


		public PageConnection getPages(){
			return pages;
		}
		public void setPages(PageConnection pages){
			this.pages = pages;
		}


		public Publication getPublication(){
			return publication;
		}
		public void setPublication(Publication publication){
			this.publication = publication;
		}


		public PublicationMapping getPublicationMapping(){
			return publicationMapping;
		}
		public void setPublicationMapping(PublicationMapping publicationMapping){
			this.publicationMapping = publicationMapping;
		}


		public PublicationConnection getPublications(){
			return publications;
		}
		public void setPublications(PublicationConnection publications){
			this.publications = publications;
		}


		public StructureGroup getStructureGroup(){
			return structureGroup;
		}
		public void setStructureGroup(StructureGroup structureGroup){
			this.structureGroup = structureGroup;
		}


		public StructureGroupConnection getStructureGroups(){
			return structureGroups;
		}
		public void setStructureGroups(StructureGroupConnection structureGroups){
			this.structureGroups = structureGroups;
		}


		public TaxonomySitemapItem getSitemap(){
			return sitemap;
		}
		public void setSitemap(TaxonomySitemapItem sitemap){
			this.sitemap = sitemap;
		}


		public TaxonomySitemapItem getSitemapSubtree(){
			return sitemapSubtree;
		}
		public void setSitemapSubtree(TaxonomySitemapItem sitemapSubtree){
			this.sitemapSubtree = sitemapSubtree;
		}
	
}
