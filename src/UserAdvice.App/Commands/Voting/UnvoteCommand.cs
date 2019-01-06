using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using UserAdvice.Data;
using UserAdvice.Data.Entities;
using UserAdvice.Utilities;

namespace UserAdvice.Commands.Voting
{
    public class UnvoteCommand : ICommand
    {
        public UnvoteCommand(int postId, ClaimsPrincipal currentUser)
        {
            if (postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            PostId = postId;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int PostId { get; }
        public ClaimsPrincipal CurrentUser { get; private set; }
    }

    internal class UnvoteCommandHandler : ICommandHandler<UnvoteCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClock _clock;

        public UnvoteCommandHandler(ApplicationDbContext dbContext,
            UserManager<AppUser> userManager, IClock clock)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _clock = clock;
        }

        public async Task Execute(UnvoteCommand command)
        {
            var user = await _userManager.GetUserAsync(command.CurrentUser);
            var userVote = await _dbContext.UserVotes
                .FirstOrDefaultAsync(x => x.PostId == command.PostId && x.VoterId == user.Id);

            if (userVote == null)
                return;

            using (var transaction = Transaction.BeginTransaction())
            {
                _dbContext.UserVotes.Remove(userVote);
                user.VoteCount -= 1;

                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlCommandAsync(
                    $"UPDATE [Posts] SET [VoteCount] = [VoteCount] - 1 WHERE PostId = {command.PostId}");

                transaction.Complete();
            }
        }
    }
}
