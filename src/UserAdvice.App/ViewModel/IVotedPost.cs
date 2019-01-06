namespace UserAdvice.ViewModel
{
    public interface IVotedPost
    {
        int Id { get; }

        int VoteCount { get; }
        bool UserVoted { get; }
    }
}
