using ZooSimulator.Api.Domain;

namespace ZooSimulator.Api.Services
{
    public interface IClockService
    {
        Clock CreateClock();
    }
    public class ClockService : IClockService
    {
        public ClockService()
        {
            
        }

        public Clock CreateClock()
        {
            return new Clock(20, new TimeOnly(9, 0));
        }
    }
}
