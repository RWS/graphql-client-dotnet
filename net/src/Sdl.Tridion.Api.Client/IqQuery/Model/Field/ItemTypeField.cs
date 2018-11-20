namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// ItemType Field
    /// </summary>
    public class ItemTypeField : BaseField
    {
        public string ItemType { get; set; }
        public ItemTypeField(bool negate, string itemType) : base(negate)
        {
            ItemType = itemType;
        }
    }
}
