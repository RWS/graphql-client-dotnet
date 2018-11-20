using Sdl.Tridion.Api.IqQuery.Model.Field;

namespace Sdl.Tridion.Api.IqQuery.Model.Search
{
    /// <summary>
    /// Search Node
    /// </summary>
    public class SearchNode
    {
        public enum SearchNodeType
        {
            Nil,
            Id,
            MultiMatch,
            ItemType,
            Field,
            Range,
            Grouped,
            Query
        }

        public IdField Id { get; protected set; }
        public ItemTypeField ItemType { get; protected set; }
        public SingleField Field { get; protected set; }
        public RangeField Range { get; protected set; }
        public GroupedField Grouped { get; protected set; }
        public IQuery Query { get; protected set; }
        public SearchNodeType Type { get; protected set; }
        public MultiMatchField MultiMatch { get; protected set; }

        private SearchNode()
        {
            Type = SearchNodeType.Nil;
        }

        private SearchNode(IdField id)
        {
            Id = id;
            Type = SearchNodeType.Id;
        }

        private SearchNode(ItemTypeField itemType)
        {
            ItemType = itemType;
            Type = SearchNodeType.ItemType;
        }

        private SearchNode(SingleField field)
        {
            Field = field;
            Type = SearchNodeType.Field;
        }

        private SearchNode(RangeField range)
        {
            Range = range;
            Type = SearchNodeType.Range;
        }

        private SearchNode(GroupedField grouped)
        {
            Grouped = grouped;
            Type = SearchNodeType.Grouped;
        }

        private SearchNode(IQuery query)
        {
            Query = query;
            Type = SearchNodeType.Query;
        }

        private SearchNode(MultiMatchField mmField)
        {
            MultiMatch = mmField;
            Type = SearchNodeType.MultiMatch;
        }

        public bool IsNil => Type == SearchNodeType.Nil;

        public static SearchNode FieldSearchNode(SingleField singleField)
        {
            return new SearchNode(singleField);
        }

        public static SearchNode QuerySearchNode(IQuery query)
        {
            return new SearchNode(query);
        }

        public static SearchNode RangeSearchNode(RangeField rangeField)
        {
            return new SearchNode(rangeField);
        }

        public static SearchNode GroupedSearchNode(GroupedField groupedField)
        {
            return new SearchNode(groupedField);
        }

        public static SearchNode IdSearchNode(IdField idField)
        {
            return new SearchNode(idField);
        }

        public static SearchNode MultiMatchSearchNode(MultiMatchField mmField)
        {
            return new SearchNode(mmField);
        }

        public static SearchNode ItemTypeSearchNode(ItemTypeField itemTypeField)
        {
            return new SearchNode(itemTypeField);
        }

        public static SearchNode NilSearchNode()
        {
            return new SearchNode();
        }
    }
}
