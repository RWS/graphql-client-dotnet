namespace Sdl.Tridion.Api.IqQuery.Model.Operation
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
