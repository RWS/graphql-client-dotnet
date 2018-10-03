using Sdl.Web.IQQuery.API;

namespace Sdl.Web.IQQuery.Model.Operation
{
    /// <summary>
    /// Or Operation
    /// </summary>
    public class OrOperation : BaseOperation
    {
        public OrOperation(IQuery query) : base(query, BooleanOperationType.Or)
        {
        }
    }
}
