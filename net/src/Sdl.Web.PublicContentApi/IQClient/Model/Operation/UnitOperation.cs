using Sdl.Web.IQQuery.API;

namespace Sdl.Web.IQQuery.Model.Operation
{
    /// <summary>
    /// Unit Operation
    /// </summary>
    public class UnitOperation : BaseOperation
    {
        public UnitOperation(IQuery query) : base(query, BooleanOperationType.Unit)
        {
        }
    }
}
