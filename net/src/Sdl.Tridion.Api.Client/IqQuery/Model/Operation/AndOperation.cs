namespace Sdl.Tridion.Api.IqQuery.Model.Operation
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
