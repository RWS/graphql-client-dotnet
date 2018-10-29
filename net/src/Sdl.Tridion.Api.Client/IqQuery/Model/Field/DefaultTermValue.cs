namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Default implementation for TermValue
    /// </summary>
    public class DefaultTermValue : ITermValue
    {
        public static readonly double NoBoost = 0;

        public object Value { get; set; }

        public TermTypes TermType { get; set; }       

        public double BoostValue { get; set; } = NoBoost;

        public DefaultTermValue()
        {
            TermType = TermTypes.Exact;
        }

        public DefaultTermValue(object value)
        {
            Value = value;
            TermType = TermTypes.Exact;
        }

        public DefaultTermValue(object value, TermTypes type)
        {
            Value = value;
            TermType = type;
        }

        public DefaultTermValue(object value, double boostValue, TermTypes type)
        {
            Value = value;
            BoostValue = boostValue;
            TermType = type;
        }

        public ITermValue Fuzzy(object value)
        {
            Value = value;
            TermType = TermTypes.Fuzzy;
            return this;
        }

        public ITermValue Exact(object value)
        {
            Value = value;
            TermType = TermTypes.Exact;
            return this;
        }

        public ITermValue WildCard(object value)
        {
            Value = value;
            TermType = TermTypes.Wildcard;
            return this;
        }

        public ITermValue Boost(double boostValue)
        {
            BoostValue = boostValue;
            return this;
        }
    }
}
