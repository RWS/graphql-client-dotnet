using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sdl.Tridion.Api.IqQuery;
using Sdl.Tridion.Api.IqQuery.Model.Field;
using Sdl.Tridion.Api.IqQuery.Model.Search;

namespace Sdl.Tridion.Api.IqQuery.Model.Compile
{
    /// <summary>
    /// POCO objects used to hold the serializable query-dsl
    /// </summary>

    #region IQueryModelTransform
    internal interface IQueryModelTransform { }
    #endregion
    #region SearchQueryModelTransformBase
    internal abstract class SearchQueryModelTransformBase : IQueryModelTransform
    {
        protected SearchQueryModelTransformBase(string typeName)
        {
            Type = typeName;
        }
        protected SearchQueryModelTransformBase(string typeName, bool negate) : this(typeName)
        {
            if(negate) Negate = true;
        }
        [JsonProperty(Order = 1)]
        public string Type { get; set; }
        [JsonProperty(Order = 2)]
        public bool? Negate { get; set; }
    }
    #endregion
    #region SearchQueryTransformer
    internal class SearchQueryTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public BooleanOperationType? Op { get; }
        [JsonProperty(Order = 4)]
        public List<string> Sort { get; }
        [JsonProperty(Order = 5)]
        public bool? Descending { get; }
        [JsonProperty(Order = 6)]
        public List<IQueryModelTransform> Nodes { get; }
        public SearchQueryTransformer(SearchQuery query) : base("query")
        {
            Op = query.BooleanOperation;
            Nodes = new List<IQueryModelTransform>();
            if (query.SortFields.Count > 0)
            {
                Sort = new List<string>();
                Sort.AddRange(query.SortStrings ? query.SortFields.Select(x => $"{x}.keyword") : query.SortFields);
            }
            if (query.SortDescending)
            {
                Descending = query.SortDescending;
            }
        }
    }
    #endregion
    #region ItemTypeFieldTransformer
    internal class ItemTypeFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public string Key { get; set; }
        public ItemTypeFieldTransformer(ItemTypeField field) : base("itemType", field.Negate)
        {
            Key = field.ItemType;
        }
    }
    #endregion
    #region IdFieldTransformer
    internal class IdFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public string Key { get; set; }
        public IdFieldTransformer(IdField field) : base("id", field.Negate)
        {
            Key = field.Id;
        }
    }
    #endregion
    #region MultiMatchFieldTransformer
    internal class MultiMatchFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public string Query { get; set; }
        [JsonProperty(Order = 4)]
        public List<string> Fieldlist { get; set; }
        public MultiMatchFieldTransformer(MultiMatchField field) : base("multiMatch", field.Negate)
        {
            Query = field.Query;
            Fieldlist = field.Fields;
        }
    }
    #endregion
    #region GroupedFieldTransformer
    internal class GroupedFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public List<string> Keys { get; set; }
        [JsonProperty(Order = 4)]
        public List<object> Values { get; set; }
        [JsonProperty(Order = 5)]
        public List<TermTypes> TermTypes { get; set; }
        [JsonProperty(Order = 6)]
        public List<double> BoostValues { get; set; }
        [JsonProperty(Order = 7)]
        public List<EntityFieldType> Types { get; set; } 
        public GroupedFieldTransformer(GroupedField field) : base("group", field.Negate)
        {
            Keys = field.Names;
            Types = field.FieldTypes.Distinct().ToList();
            Values = new List<object>();         
            if (field.IsTerm)
            {
                TermTypes = new List<TermTypes>();
                BoostValues = new List<double>();
                foreach (var x in field.TermValues)
                {
                    TermTypes.Add(x.TermType);
                    BoostValues.Add(x.BoostValue);
                    Values.Add(x.Value);
                }
            }
            else
            {
                Values.AddRange(field.Values);
            } 
        }
    }
    #endregion
    #region RangeFieldTransformer
    internal class RangeFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public string Key { get; set; }
        [JsonProperty(Order = 4)]
        public object Left { get; set; }
        [JsonProperty(Order = 5)]
        public object Right { get; set; }
        [JsonProperty(Order = 6)]
        public bool LeftClosed { get; set; }
        [JsonProperty(Order = 7)]
        public bool RightClosed { get; set; }
        [JsonProperty(Order = 8)]
        public EntityFieldType FieldType { get; set; }

        public RangeFieldTransformer(RangeField field) : base("range", field.Negate)
        {
            FieldType = field.FieldType;
            Key = field.Name;
            Left = field.Left;
            Right = field.Right;
            LeftClosed = field.LeftClosed;
            RightClosed = field.RightClosed;
        }
    }
    #endregion
    #region SingleSearchFieldTransformer
    internal class SingleSearchFieldTransformer : SearchQueryModelTransformBase
    {
        [JsonProperty(Order = 3)]
        public string Key { get; set; }
        [JsonProperty(Order = 4)]
        public object Value { get; set; }
        [JsonProperty(Order = 5)]
        public TermTypes? TermType { get; }
        [JsonProperty(Order = 6)]
        public double? Boost { get; }
        [JsonProperty(Order = 7)]
        public EntityFieldType FieldType { get; set; }

        public SingleSearchFieldTransformer(SingleField field) : base("field", field.Negate)
        {
            Key = field.Name;
            if (field.TermValue != null)
            {
                Value = field.TermValue.Value;
                TermType = field.TermValue.TermType;

                if (field.TermValue.BoostValue != DefaultTermValue.NoBoost)
                {
                    Boost = field.TermValue.BoostValue;
                }
                else
                {
                    Boost = null;
                }
            }
            else
            {
                Value = field.Value;
                FieldType = FieldUtils.DetectFieldType(field.Value);
            }
        }
    }
    #endregion
}
