using Sdl.Tridion.Api.IQQuery.API;

namespace Sdl.Tridion.Api.IQQuery.Model.Operation
{
    /// <summary>
    /// And Operation
    /// </summary>
    public class AndOperation : BaseOperation
    {
        public AndOperation(IQuery query) : base(query, BooleanOperationType.And)
        {
        }
    }
}
