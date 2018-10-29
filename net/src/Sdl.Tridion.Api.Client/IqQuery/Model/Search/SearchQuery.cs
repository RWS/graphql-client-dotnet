using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sdl.Tridion.Api.IqQuery.Model.Field;
using Sdl.Tridion.Api.IqQuery.Model.Operation;

namespace Sdl.Tridion.Api.IqQuery.Model.Search
{
    /// <summary>
    /// Search Query
    /// </summary>
    public class SearchQuery : IQuery
    {
        private BaseOperation _operation;

        public IBooleanOperation Operation => _operation;

        public SearchNode Left { get; protected set; } = SearchNode.NilSearchNode();

        public SearchNode Right { get; protected set; } = SearchNode.NilSearchNode();

        public IQuery Parent { get; protected set; }

        public bool NegateNext { get; protected set; }

        public List<string> SortFields { get; protected set; } = new List<string>();

        public bool SortDescending { get; protected set; }

        public bool SortStrings { get; protected set; }

        public SearchQuery()
        {
        }

        private SearchQuery(SearchQuery parent)
        {
            Parent = parent;
        }

        public IQuery GroupStart()
        {
            SearchNode child = SearchNode.QuerySearchNode(new SearchQuery(this));
            SetFieldInOrder(child);
            return child.Query;
        }

        public IQuery Not()
        {
            NegateNext = true;
            return this;
        }

        public BooleanOperationType? BooleanOperation => Operation == null ? (BooleanOperationType?) null : _operation.OperationType;

        public IBooleanOperation Id(string id) => ApplyUnaryNode(SearchNode.IdSearchNode(new IdField(NegateNext, id)), new UnitOperation(this));

        public IBooleanOperation Id(string sourceIdentifier, int publicationId, int itemId, int itemType) => Id($"{sourceIdentifier}:{publicationId}-{itemId}-{itemType}");

        public IBooleanOperation MultiMatch(string query) => ApplyUnaryNode(SearchNode.MultiMatchSearchNode(new MultiMatchField(NegateNext, query)), new OrOperation(this));

        public IBooleanOperation MultiMatch(List<string> wildCardFieldNames, string query) => ApplyUnaryNode(SearchNode.MultiMatchSearchNode(new MultiMatchField(NegateNext, query, wildCardFieldNames)), new OrOperation(this));

        public IBooleanOperation MultiMatch(List<string> wildCardFieldNames, string query, MatchOperation operation)
        {
            BaseOperation baseOperation = operation == MatchOperation.And
                ? (BaseOperation) new AndOperation(this)
                : new OrOperation(this);
            return ApplyUnaryNode(SearchNode.MultiMatchSearchNode(
                    new MultiMatchField(NegateNext, query, wildCardFieldNames)), baseOperation);
        }

        public IBooleanOperation ItemType(string itemType) => ApplyBinaryNode(SearchNode.ItemTypeSearchNode(new ItemTypeField(NegateNext, itemType)));

        public IBooleanOperation Field(string fieldName, string fieldValue) => ApplyBinaryNode(SearchNode.FieldSearchNode(new SingleField(NegateNext, fieldName, fieldValue)));

        public IBooleanOperation Field(string fieldName, ITermValue fieldValue) => ApplyBinaryNode(SearchNode.FieldSearchNode(new SingleField(NegateNext, fieldName, fieldValue)));

        public IBooleanOperation Field(string fieldName, object fieldValue) => ApplyBinaryNode(SearchNode.FieldSearchNode(new SingleField(NegateNext, fieldName, fieldValue)));

        public IBooleanOperation Range(string fieldName, DateTimeOffset lower, DateTimeOffset upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Date, lower, upper)));

        public IBooleanOperation Range(string fieldName, DateTimeOffset lower, DateTimeOffset upper, bool includeLower,
            bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Date, lower, includeLower, upper, includeUpper)));

        public IBooleanOperation Range(string fieldName, int lower, int upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Integer, lower, upper)));

        public IBooleanOperation Range(string fieldName, int lower, int upper, bool includeLower, bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Integer, lower, includeLower, upper, includeUpper)));

        public IBooleanOperation Range(string fieldName, double lower, double upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Double, lower, upper)));

        public IBooleanOperation Range(string fieldName, double lower, double upper, bool includeLower, bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Double, lower, includeLower, upper, includeUpper)));

        public IBooleanOperation Range(string fieldName, float lower, float upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Float, lower, upper)));

        public IBooleanOperation Range(string fieldName, float lower, float upper, bool includeLower, bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Float, lower, includeLower, upper, includeUpper)));

        public IBooleanOperation Range(string fieldName, long lower, long upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Long, lower, upper)));

        public IBooleanOperation Range(string fieldName, long lower, long upper, bool includeLower, bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.Long, lower, includeLower, upper, includeUpper)));

        public IBooleanOperation Range(string fieldName, string lower, string upper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.String, lower, upper)));

        public IBooleanOperation Range(string fieldName, string lower, string upper, bool includeLower, bool includeUpper) => ApplyBinaryNode(SearchNode.RangeSearchNode(new RangeField(NegateNext, fieldName, EntityFieldType.String, lower, includeLower, upper, includeUpper)));

        public IOperation GroupedAnd(List<string> fields, IList query) => GroupedOperation(fields, query, new AndOperation(this), NegateNext);

        public IOperation GroupedOr(List<string> fields, IList query) => GroupedOperation(fields, query, new OrOperation(this), NegateNext);

        public IOperation GroupedNot(List<string> fields, IList query) => GroupedOperation(fields, query, new AndOperation(this), true);

        public IQuery WithOperation(IBooleanOperation operation)
        {
            _operation = (BaseOperation)operation;
            return this;
        }

        public IQuery SortFieldsDescending(List<string> fieldNames)
        {
            SortDescending = true;
            SortFields = fieldNames;
            return this;
        }

        public IQuery SortFieldsAscending(List<string> fieldNames)
        {
            SortDescending = false;
            SortFields = fieldNames;
            return this;
        }

        public IQuery SortStringFieldsDescending(List<string> fieldNames)
        {
            SortStrings = true;
            SortFieldsDescending(fieldNames);
            return this;
        }

        public IQuery SortStringFieldsAscending(List<string> fieldNames)
        {
            SortStrings = true;
            SortFieldsAscending(fieldNames);
            return this;
        }

        public IQuery GroupEnd()
        {
            //if(Parent == null) throw new QueryException("Group not started");
            return Parent;
        }

        private BaseOperation ApplyUnaryNode(SearchNode node, BaseOperation op)
        {
            if (!Left.IsNil)
            {
                throw new QueryException("Can not use this field inside binary operation");
            }
            Left = node;
            _operation = op;
            NegateNext = false;
            return _operation;
        }

        IBooleanOperation ApplyBinaryNode(SearchNode node)
        {
            SetFieldInOrder(node);
            NegateNext = false;
            return _operation ?? (_operation = new UnitOperation(this));
        }

        IBooleanOperation GroupedOperation(IList fields, IList query, BaseOperation baseOp, bool negate)
        {
            CheckCollectionSize(fields);
            CheckCollectionSize(query);
            var x = query[0];
            if (query[0].GetType().IsAssignableFrom(typeof (ITermValue)))
            {
                if (query.Cast<object>().Any(y => !x.GetType().IsAssignableFrom(typeof(ITermValue))))
                {
                    throw new QueryException("All elements must be either TermValue instances or not");
                }

                return ApplyUnaryNode(SearchNode.GroupedSearchNode(new GroupedField(negate, (List<string>)fields, (List<object>)query)), baseOp);
            }

            if (query.Cast<object>().Any(y => x.GetType().IsAssignableFrom(typeof(ITermValue))))
            {
                throw new QueryException("All elements must be either TermValue instances or not");
            }

            return ApplyUnaryNode(SearchNode.GroupedSearchNode(new GroupedField(negate, (List<string>)fields, (List<object>)query)), baseOp);
        }

        private static void CheckCollectionSize(IList collection)
        {
            if (null == collection || collection.Count == 0)
            {
                throw new QueryException("Collection is empty");
            }
        }

        private void SetFieldInOrder(SearchNode field)
        {
            if (Left.IsNil)
            {
                Left = field;
            }
            else
            {
                if (Right.IsNil)
                {
                    Right = field;
                }
                else
                {
                    throw new QueryException("All fields are set!");
                }
            }
        }
    }
}
