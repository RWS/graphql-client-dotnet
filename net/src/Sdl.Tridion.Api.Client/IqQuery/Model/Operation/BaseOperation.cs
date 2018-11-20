using System.Collections.Generic;
using Sdl.Tridion.Api.IqQuery.Model.Compile;
using Sdl.Tridion.Api.IqQuery.Model.Search;

namespace Sdl.Tridion.Api.IqQuery.Model.Operation
{
    public abstract class BaseOperation : IBooleanOperation
    {
        private readonly IQuery _query;
        private readonly IQueryCompiler _compiler;

        protected BaseOperation(IQuery query, BooleanOperationType type)
        {
            _query = query;
            OperationType = type;
            // the compiler will build our query-dsl. switch it out to custom impl if you wish to build your own query service
            _compiler = new DefaultQueryCompiler();
        }

        public BooleanOperationType OperationType { get; }

        public ICriteria Compile()
        {
            try
            {
                return new SearchCriteria(_compiler.Compile(SearchNode.QuerySearchNode(_query)));
            }
            catch (CompileException e)
            {
                throw new QueryException("Compilation failed", e);
            }
        }

        public IBooleanOperation SortByAscending(List<string> fieldNames)
        {
            _query.SortFieldsAscending(fieldNames);
            return this;
        }

        public IBooleanOperation SortByDescending(List<string> fieldNames)
        {
            _query.SortFieldsDescending(fieldNames);
            return this;
        }

        public IBooleanOperation SortStringByAscending(List<string> fieldNames)
        {
            _query.SortStringFieldsAscending(fieldNames);
            return this;
        }

        public IBooleanOperation SortStringByDescending(List<string> fieldNames)
        {
            _query.SortStringFieldsDescending(fieldNames);
            return this;
        }

        public IQuery And()
        {
            return _query.WithOperation(new AndOperation(_query));
        }

        public IQuery Or()
        {
            return _query.WithOperation(new OrOperation(_query));
        }

        public IBooleanOperation GroupEnd()
        {
            return new UnitOperation(_query.GroupEnd());
        }
    }
}
