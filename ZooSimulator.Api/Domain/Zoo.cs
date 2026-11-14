using ZooSimulator.Api.Helpers;

namespace ZooSimulator.Api.Domain
{
    public class Zoo
    {
        public const int DefaultAnimalsPerType = 5;

        private readonly List<Animal> _animals;
        private readonly Randomiser _healthReductionRandomiser;
        private readonly Randomiser _feedRandomiser;

        public IReadOnlyList<Animal> Animals => _animals;
        public TimeOnly Time { get; private set; } = new TimeOnly(9, 0);


        public Zoo(IEnumerable<Animal> animals, Randomiser? healthReductionRandomiser = null, Randomiser? feedRandomiser = null)
        {
            _animals = animals.ToList();
            _healthReductionRandomiser = healthReductionRandomiser ?? new Randomiser(0, 20);
            _feedRandomiser = feedRandomiser ?? new Randomiser(10, 25);
        }   

        public static Zoo CreateDefault()
        {
            var animals = new List<Animal>();

            for (var i = 0; i < DefaultAnimalsPerType; i++) { animals.Add(new Elephant()); }
            for (var i = 0; i < DefaultAnimalsPerType; i++) { animals.Add(new Giraffe()); }
            for (var i = 0; i < DefaultAnimalsPerType; i++) { animals.Add(new Monkey()); }

            return new Zoo(animals);
        }

        public void ProcessIntervalElapsed()
        {
            Time = Time.AddHours(1);

            foreach (var animal in _animals)
            {
                var newHealthRandomPercentage = _healthReductionRandomiser.GetRandomNumber();
                animal.IntervalElapsedActivities(newHealthRandomPercentage);
            }
        }

        public void Feed()
        {
            foreach (var animalType in _animals.GroupBy(a => a.Type))
            {
                var newFeedRandomPercentage = _feedRandomiser.GetRandomNumber();
                foreach (var animal in animalType)
                {
                    animal.Feed(newFeedRandomPercentage);
                }
            }
        }
    }
}