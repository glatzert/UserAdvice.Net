using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UserAdvice.Data;
using UserAdvice.Data.Entities;
using UserAdvice.Utilities;

namespace UserAdvice.Commands.Voting
{
    public class VoteCommand : ICommand
    {
        public VoteCommand(int postId, ClaimsPrincipal currentUser)
        {
            if (postId <= 0)
                throw new ArgumentOutOfRangeException(nameof(postId));

            PostId = postId;
            CurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        public int PostId { get; }
        public ClaimsPrincipal CurrentUser { get; private set; }
    }

    internal class VoteCommandHandler : ICommandHandler<VoteCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IClock _clock;

        public VoteCommandHandler(ApplicationDbContext dbContext,
            UserManager<AppUser> userManager, IClock clock)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _clock = clock;
        }

        public async Task Execute(VoteCommand command)
        {
            var user = await _userManager.GetUserAsync(command.CurrentUser);
            var exists = await _dbContext.UserVotes
                .AnyAsync(x => x.PostId == command.PostId && x.VoterId == user.Id);

            if (exists)
                return;

            using (var transaction = Transaction.BeginTransaction())
            {
                var userVote = new UserVote
                {
                    PostId = command.PostId,
                    VoterId = user.Id,
                    VotedAt = _clock.Now,
                };

                _dbContext.UserVotes.Add(userVote);
                user.VoteCount += 1;

                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.ExecuteSqlCommandAsync(
                    $"UPDATE [Posts] SET [VoteCount] = [VoteCount] + 1 WHERE PostId = {command.PostId}");

                transaction.Complete();
            }
        }
    }
}
