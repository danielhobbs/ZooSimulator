namespace ZooSimulator.Api.Domain
{
    public class Clock
    {
        public int IntervalInSeconds { get; set; } = 20;

        public TimeOnly Time { get; set; }

        public Clock(int intervalInSeconds, TimeOnly time)
        {
            IntervalInSeconds = intervalInSeconds;
            Time = time;
        }
    }
}
