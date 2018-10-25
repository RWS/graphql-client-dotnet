using Sdl.Tridion.Api.IQQuery.API;

namespace Sdl.Tridion.Api.IQQuery.Model.Operation
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
