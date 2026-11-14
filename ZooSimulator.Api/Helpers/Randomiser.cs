namespace ZooSimulator.Api.Helpers
{
    public class Randomiser
    {
        private readonly Random _random;
        private readonly int _min;
        private readonly int _max;

        public Randomiser(int min, int max)
        {
            _random = new Random();
            _min = min;
            _max = max;
        }

        public int GetRandomNumber()
        {
            return _random.Next(_min, _max + 1);
        }
    }
}
