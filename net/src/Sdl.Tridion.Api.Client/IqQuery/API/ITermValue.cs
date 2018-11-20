namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Represents a value to search for.
    /// </summary>
    public interface ITermValue
    {
        TermTypes TermType { get; }

        object Value { get; }

        double BoostValue { get; }

        ITermValue Fuzzy(object value);

        ITermValue Exact(object value);

        ITermValue WildCard(object value);

        ITermValue Boost(double boostValue);
    }
}
