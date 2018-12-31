using System;

namespace UserAdvice.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}