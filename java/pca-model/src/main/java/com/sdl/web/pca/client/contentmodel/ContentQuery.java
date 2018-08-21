package com.sdl.web.pca.client.contentmodel;

/**
*The query root for the GraphQL Public Content API.
*/
public class ContentQuery{

		public BinaryComponent binaryComponent;

		public KeywordConnection categories;

		public ComponentPresentation componentPresentation;

		public ItemConnection items;

		public Keyword keyword;

		public Page page;

		public PageConnection pages;

		public Publication publication;

		public PublicationConnection publications;

		public StructureGroup structureGroup;

		public StructureGroupConnection structureGroups;

		public TaxonomySitemapItem sitemap;

		public TaxonomySitemapItem sitemapSubtree;
	
}
