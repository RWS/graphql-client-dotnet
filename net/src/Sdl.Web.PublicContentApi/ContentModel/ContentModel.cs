// This file was generated by a tool on 15/06/2018 16:08:24
using System.Collections;
using System.Collections.Generic;
namespace Sdl.Web.PublicContentApi.ContentModel
{
	public class ContentQuery	{
		public BinaryComponent BinaryComponent { get; set; }
		public KeywordConnection Categories { get; set; }
		public ComponentPresentation ComponentPresentation { get; set; }
		public Keyword Keyword { get; set; }
		public Page Page { get; set; }
		public PageConnection Pages { get; set; }
		public Publication Publication { get; set; }
		public PublicationConnection Publications { get; set; }
		public StructureGroup StructureGroup { get; set; }
		public StructureGroupConnection StructureGroups { get; set; }
	}
	public class BinaryComponent : IItem	{
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public int PublicationId { get; set; }
		public int SchemaId { get; set; }
		public List<ITaxonomyItem> Taxonomies { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
		public string UpdatedDate { get; set; }
		public BinaryVariantConnection Variants { get; set; }
		public bool MultiMedia { get; set; }
	}
	public class CustomMetaConnection	{
		public List<CustomMetaEdge> Edges { get; set; }
	}
	public class CustomMetaEdge	{
		public CustomMeta Node { get; set; }
		public string Cursor { get; set; }
	}
	public class CustomMeta	{
		public string Id { get; set; }
		public int ItemId { get; set; }
		public string Key { get; set; }
		public int NamespaceId { get; set; }
		public int PublicationId { get; set; }
		public string Value { get; set; }
		public CustomMetaValueType ValueType { get; set; }
	}
	public enum CustomMetaValueType	{
		STRING,
		DATE,
		FLOAT,
		NUMBER,
		UNKNOWN
	}
	public interface ITaxonomyItem	{
		TaxonomyItemConnection Children { get; set; }
		string CreationDate { get; set; }
		CustomMetaConnection CustomMetas { get; set; }
		int Depth { get; set; }
		string Id { get; set; }
		string InitialPublishDate { get; set; }
		int ItemId { get; set; }
		int ItemType { get; set; }
		string Key { get; set; }
		string LastPublishDate { get; set; }
		int NamespaceId { get; set; }
		int OwningPublicationId { get; set; }
		ITaxonomyItem Parent { get; set; }
		int PublicationId { get; set; }
		int TaxonomyId { get; set; }
		TaxonomyType TaxonomyType { get; set; }
		string Title { get; set; }
		string UpdatedDate { get; set; }
		bool HasChildren { get; set; }
		bool Abstract { get; set; }
		bool Navigable { get; set; }
	}
	public class TaxonomyItemConnection	{
		public List<TaxonomyItemEdge> Edges { get; set; }
	}
	public class TaxonomyItemEdge	{
		public ITaxonomyItem Node { get; set; }
		public string Cursor { get; set; }
	}
	public enum TaxonomyType	{
		KEYWORD,
		CATEGORY,
		STRUCTUREGROUP
	}
	public class BinaryVariantConnection	{
		public List<BinaryVariantEdge> Edges { get; set; }
	}
	public class BinaryVariantEdge	{
		public BinaryVariant Node { get; set; }
		public string Cursor { get; set; }
	}
	public class BinaryVariant	{
		public int BinaryId { get; set; }
		public string BinaryUrl { get; set; }
		public string Description { get; set; }
		public string Id { get; set; }
		public string Path { get; set; }
		public string Type { get; set; }
		public string VariantId { get; set; }
	}
	public interface IContentComponent	{
		string CreationDate { get; set; }
		CustomMetaConnection CustomMetas { get; set; }
		string Id { get; set; }
		string InitialPublishDate { get; set; }
		int ItemId { get; set; }
		int ItemType { get; set; }
		string LastPublishDate { get; set; }
		int NamespaceId { get; set; }
		int OwningPublicationId { get; set; }
		int PublicationId { get; set; }
		int SchemaId { get; set; }
		List<ITaxonomyItem> Taxonomies { get; set; }
		string Title { get; set; }
		string UpdatedDate { get; set; }
		bool MultiMedia { get; set; }
	}
	public interface IItem	{
		string CreationDate { get; set; }
		CustomMetaConnection CustomMetas { get; set; }
		string Id { get; set; }
		string InitialPublishDate { get; set; }
		int ItemId { get; set; }
		int ItemType { get; set; }
		string LastPublishDate { get; set; }
		int NamespaceId { get; set; }
		int OwningPublicationId { get; set; }
		int PublicationId { get; set; }
		string Title { get; set; }
		string UpdatedDate { get; set; }
	}
	public enum ClaimValueType	{
		STRING,
		DATE,
		FLOAT,
		NUMBER,
		BOOLEAN
	}
	public class KeywordConnection	{
		public List<KeywordEdge> Edges { get; set; }
	}
	public class KeywordEdge	{
		public Keyword Node { get; set; }
		public string Cursor { get; set; }
	}
	public class Keyword : IItem	{
		public TaxonomyItemConnection Children { get; set; }
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public int Depth { get; set; }
		public string Description { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string Key { get; set; }
		public string LastPublishDate { get; set; }
		public string Name { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public ITaxonomyItem Parent { get; set; }
		public int PublicationId { get; set; }
		public int TaxonomyId { get; set; }
		public TaxonomyType TaxonomyType { get; set; }
		public string Title { get; set; }
		public int TotalRelatedItems { get; set; }
		public string UpdatedDate { get; set; }
		public bool HasChildren { get; set; }
		public bool Abstract { get; set; }
		public bool Navigable { get; set; }
		public bool UsedForIdentification { get; set; }
	}
	public class ComponentPresentation : IItem	{
		public IContentComponent Component { get; set; }
		public Template ComponentTemplate { get; set; }
		public IContent Content { get; set; }
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public int PublicationId { get; set; }
		public RawContent RawContent { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
	}
	public class Template : IItem	{
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public string OutputFormat { get; set; }
		public int OwningPublicationId { get; set; }
		public int Priority { get; set; }
		public int PublicationId { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
	}
	public interface IContent	{
		string Id { get; set; }
		string Type { get; set; }
	}
	public class RawContent	{
		public string CharSet { get; set; }
		public string Content { get; set; }
		public IDictionary Data { get; set; }
		public string Id { get; set; }
	}
	public interface IContentFragment	{
		IContent Content { get; set; }
		string CreationDate { get; set; }
		CustomMetaConnection CustomMetas { get; set; }
		string Id { get; set; }
		string InitialPublishDate { get; set; }
		int ItemId { get; set; }
		int ItemType { get; set; }
		string LastPublishDate { get; set; }
		int NamespaceId { get; set; }
		int OwningPublicationId { get; set; }
		int PublicationId { get; set; }
		string Title { get; set; }
		string UpdatedDate { get; set; }
	}
	public class Page : IContainer, IItem	{
		public List<IItem> ContainerItems { get; set; }
		public IContent Content { get; set; }
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string FileName { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public Template PageTemplate { get; set; }
		public int PublicationId { get; set; }
		public RawContent RawContent { get; set; }
		public List<ITaxonomyItem> Taxonomies { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
		public string Url { get; set; }
	}
	public enum ContainerType	{
		COMPONENT_PRESENTATION
	}
	public interface IContainer	{
		List<IItem> ContainerItems { get; set; }
	}
	public interface ITaggable	{
		List<ITaxonomyItem> Taxonomies { get; set; }
	}
	public class PageConnection	{
		public List<PageEdge> Edges { get; set; }
	}
	public class PageEdge	{
		public Page Node { get; set; }
		public string Cursor { get; set; }
	}
	public class Publication : IItem	{
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public string MultimediaPath { get; set; }
		public string MultimediaUrl { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public int PublicationId { get; set; }
		public string PublicationKey { get; set; }
		public string PublicationPath { get; set; }
		public string PublicationUrl { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
	}
	public class PublicationConnection	{
		public List<PublicationEdge> Edges { get; set; }
	}
	public class PublicationEdge	{
		public Publication Node { get; set; }
		public string Cursor { get; set; }
	}
	public enum ContentFilterQueryType	{
		CUSTOM_META
	}
	public class StructureGroup : IItem	{
		public TaxonomyItemConnection Children { get; set; }
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public int Depth { get; set; }
		public string Directory { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string Key { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public ITaxonomyItem Parent { get; set; }
		public int PublicationId { get; set; }
		public int TaxonomyId { get; set; }
		public TaxonomyType TaxonomyType { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
		public bool HasChildren { get; set; }
		public bool Abstract { get; set; }
		public bool Navigable { get; set; }
	}
	public class StructureGroupConnection	{
		public List<StructureGroupEdge> Edges { get; set; }
	}
	public class StructureGroupEdge	{
		public StructureGroup Node { get; set; }
		public string Cursor { get; set; }
	}
	public class Component : IItem	{
		public string CreationDate { get; set; }
		public CustomMetaConnection CustomMetas { get; set; }
		public string Id { get; set; }
		public string InitialPublishDate { get; set; }
		public int ItemId { get; set; }
		public int ItemType { get; set; }
		public string LastPublishDate { get; set; }
		public int NamespaceId { get; set; }
		public int OwningPublicationId { get; set; }
		public int PublicationId { get; set; }
		public int SchemaId { get; set; }
		public List<ITaxonomyItem> Taxonomies { get; set; }
		public string Title { get; set; }
		public string UpdatedDate { get; set; }
		public bool MultiMedia { get; set; }
	}
	public class ClaimValue	{
		public ClaimValueType Type { get; set; }
		public string Uri { get; set; }
		public string Value { get; set; }
	}
	public class UntypedContent : IContent	{
		public IDictionary Data { get; set; }
		public string Id { get; set; }
		public string Type { get; set; }
	}
	public class Node	{
		public string Id { get; set; }
	}
}
