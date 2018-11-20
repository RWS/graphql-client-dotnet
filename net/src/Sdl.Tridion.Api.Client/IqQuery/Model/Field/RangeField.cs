namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Range field
    /// </summary>
    public class RangeField : BaseField
    {
        public string Name { get; set; }

        public EntityFieldType FieldType { get; set; }

        public object Left { get; set; }

        public bool LeftClosed { get; set; }

        public object Right { get; set; }

        public bool RightClosed { get; set; }

        public RangeField(bool negate, string name, EntityFieldType type, object left, object right) 
            : this(negate, name, type, left, false, right, false)
        {            
        }

        public RangeField(bool negate, string name, EntityFieldType type, object left, bool leftClosed,
                          object right, bool rightClosed) : base(negate)
        {
            Name = name;
            FieldType = type;
            Left = left;
            Right = right;
            LeftClosed = leftClosed;
            RightClosed = rightClosed;
        }
    }
}
