using System.Threading.Tasks;

namespace UserAdvice.Commands
{
    /// <summary>
    /// Interface enabling a class to execute a given command.
    /// </summary>
    /// <typeparam name="ICommand">The command type to be executed.</typeparam>
    public interface ICommandHandler<ICommand>
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command to be exectued.</param>
        Task Execute(ICommand command);
    }
}
