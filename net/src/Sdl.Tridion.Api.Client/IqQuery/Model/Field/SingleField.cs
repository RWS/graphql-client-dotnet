namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Single value field.
    /// </summary>
    public class SingleField : BaseField
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ITermValue TermValue { get; set; }
        public EntityFieldType FieldType { get; set; }

        public SingleField(bool negate, string name, string value) : base(negate)
        {
            Name = name;
            Value = value;
            FieldType = EntityFieldType.String;
        }

        public SingleField(bool negate, string name, object value) : base(negate)
        {
            Name = name;
            Value = value;
            FieldType = FieldUtils.DetectFieldType(value);
        }

        public SingleField(bool negate, string name, ITermValue term) : base(negate)
        {
            Name = name;
            TermValue = term;
            FieldType = FieldUtils.DetectFieldType(term.Value);
        }
    }
}
