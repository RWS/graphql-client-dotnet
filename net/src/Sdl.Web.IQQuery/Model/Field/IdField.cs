namespace Sdl.Web.IQQuery.Model.Field
{
    /// <summary>
    /// Id Field
    /// </summary>
    public class IdField : BaseField
    {
        public string Id { get; set; }

        public IdField(bool negate, string id) : base(negate)
        {
            Id = id;
        }
    }
}
