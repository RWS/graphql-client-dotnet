using Sdl.Tridion.Api.IQQuery.API;

namespace Sdl.Tridion.Api.IQQuery.Model.Operation
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
