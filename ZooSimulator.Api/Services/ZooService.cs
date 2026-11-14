using ZooSimulator.Api.Domain;

namespace ZooSimulator.Api.Services
{
    public interface IZooService
    {
        Zoo GetZoo();
        void ProcessInterval();
        void FeedAnimals();
    }

    public class ZooService : IZooService
    {
        private readonly Zoo _zoo;

        public ZooService()
        {
            _zoo = Zoo.CreateDefault();
        }

        public void ProcessInterval()
        {
            _zoo.ProcessIntervalElapsed();
        }
        public void FeedAnimals()
        {
            _zoo.Feed();
        }

        public Zoo GetZoo()
        {
            return _zoo;
        }
    }
}
