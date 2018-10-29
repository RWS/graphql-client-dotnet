using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Sdl.Tridion.Api.IqQuery.Model.Search;

namespace Sdl.Tridion.Api.IqQuery.Model.Compile
{
    /// <summary>
    /// Default implementation for Query Compiler.
    /// We transform the query data model into a simple query-dsl that can be passed to the iq service which will then transform that into the configured
    /// search provider such as elasticsearch.
    /// </summary>
    public class DefaultQueryCompiler : IQueryCompiler
    {
        public virtual string Compile(SearchNode node)
        {
            if (node == null) throw new CompileException("Not a valid query.");
            IQueryModelTransform structure = CompileSearchNode(node);
            if (structure == null) throw new CompileException("Failed to compile query!");
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter { CamelCaseText = false } },
            };
            return JsonConvert.SerializeObject(structure, settings);
        }

        private IQueryModelTransform CompileSearchNode(SearchNode node)
        {              
            if(node == null) throw new CompileException("SearchNode is null!");
            switch (node.Type)
            {
                case SearchNode.SearchNodeType.Nil:
                    return null;
                case SearchNode.SearchNodeType.Id:
                    return new IdFieldTransformer(node.Id);
                case SearchNode.SearchNodeType.MultiMatch:
                    return new MultiMatchFieldTransformer(node.MultiMatch);
                case SearchNode.SearchNodeType.ItemType:
                    return new ItemTypeFieldTransformer(node.ItemType);
                case SearchNode.SearchNodeType.Field:
                    return new SingleSearchFieldTransformer(node.Field);
                case SearchNode.SearchNodeType.Range:
                    return new RangeFieldTransformer(node.Range);
                case SearchNode.SearchNodeType.Grouped:
                    return new GroupedFieldTransformer(node.Grouped);
                case SearchNode.SearchNodeType.Query:
                    return CompileSearchQuery((SearchQuery)node.Query);                    
                default:
                    throw new CompileException("Unhandled node type!");
            }
        }

        private IQueryModelTransform CompileSearchQuery(SearchQuery query)
        {
            if(!query.BooleanOperation.HasValue) throw new CompileException("Operation is not specified");
            SearchQueryTransformer transformer;
            switch (query.Left.Type)
            {
                case SearchNode.SearchNodeType.Nil:
                    throw new CompileException("Query is empty.");
                case SearchNode.SearchNodeType.Id:
                case SearchNode.SearchNodeType.Grouped:
                    transformer = new SearchQueryTransformer(query);
                    transformer.Nodes.Add(CompileSearchNode(query.Left));
                    break;
                default:
                    transformer = new SearchQueryTransformer(query);
                    IQueryModelTransform s = CompileSearchNode(query.Left);
                    if (s != null)
                    {
                        transformer.Nodes.Add(s);
                    }
                    s = CompileSearchNode(query.Right);
                    if (s != null)
                    {
                        transformer.Nodes.Add(s);
                    }
                    break;
            }
            return transformer;
        }
    }
}
