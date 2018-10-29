namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Base Field
    /// </summary>
    public abstract class BaseField
    {
        public bool Negate { get; set; }

        protected BaseField(bool negate)
        {
            Negate = negate;
        }
    }
}
