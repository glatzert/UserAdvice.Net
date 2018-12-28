namespace UserAdvice.Queries
{
    /// <summary>
    /// Marker interface for Query Types
    /// </summary>
    /// <typeparam name="TResult">
    ///     The result type, which the query handler will produce for this query.
    /// </typeparam>
    public interface IQuery<TResult>
    {
    }
}
