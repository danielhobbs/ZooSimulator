using ZooSimulator.Api.Domain;

namespace ZooSimulator.Api.Services
{
    public interface IZooService
    {
        Zoo CreateZoo();
        Zoo GetZoo();
        void FeedAnimals();
    }

    public class ZooService : IZooService
    {
        public ZooService()
        {

        }

        public Zoo CreateZoo()
        {
            throw new NotImplementedException();
        }

        public void FeedAnimals()
        {
            throw new NotImplementedException();
        }

        public Zoo GetZoo()
        {
            throw new NotImplementedException();
        }
    }
}
