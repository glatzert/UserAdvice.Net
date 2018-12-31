using System;

namespace UserAdvice.Utilities
{
    public class Clock : IClock
    {
        public DateTimeOffset Now { get => DateTimeOffset.Now; }
        public DateTime Today { get => Now.Date; }
    }
}
