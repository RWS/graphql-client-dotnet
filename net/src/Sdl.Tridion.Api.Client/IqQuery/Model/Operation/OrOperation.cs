namespace Sdl.Tridion.Api.IqQuery.Model.Operation
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
