namespace Sdl.Tridion.Api.IqQuery
{
    /// <summary>
    /// Represents a Boolean Search Operation.
    /// </summary>
    public interface IBooleanOperation : IOperation
    {
        /// <summary>
        /// Sets the next operation to be AND.
        /// </summary>
        /// <returns>The Query object.</returns>
        IQuery And();

        /// <summary>
        /// Sets the next operation to be OR.
        /// </summary>
        /// <returns>The Query object.</returns>
        IQuery Or();

        /// <summary>
        /// Ends group.
        /// </summary>
        /// <returns>BooleanOperation object.</returns>
        IBooleanOperation GroupEnd();
    }
}
