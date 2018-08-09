using Sdl.Web.IQQuery.API;

namespace Sdl.Web.IQQuery.Model.Operation
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
