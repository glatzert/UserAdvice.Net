using System.Threading.Tasks;

namespace UserAdvice.Queries
{
    /// <summary>
    /// Executes a given query of type TQuery to produce a result of type TResult.
    /// </summary>
    /// <typeparam name="TQuery">The query type, which is executed by this handler.</typeparam>
    /// <typeparam name="TResult">The result type, which is produced by the execute method.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Execute the query and return the result.
        /// </summary>
        /// <param name="query">The query to be executed.</param>
        Task<TResult> Execute(TQuery query);
    }
}
