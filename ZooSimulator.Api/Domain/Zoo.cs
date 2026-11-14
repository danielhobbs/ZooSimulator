namespace ZooSimulator.Api.Domain
{
    public class Zoo
    {
        public const int AnimalsPerType = 5;

        private readonly List<Animal> _animals;

        public IReadOnlyList<Animal> Animals => _animals;

        public Zoo(IEnumerable<Animal> animals)
        {
            _animals = animals.ToList();
        }

        public static Zoo CreateDefault()
        {
            var animals = new List<Animal>();

            for (var i = 0; i < AnimalsPerType; i++) { animals.Add(new Elephant()); }
            for (var i = 0; i < AnimalsPerType; i++) { animals.Add(new Giraffe()); }
            for (var i = 0; i < AnimalsPerType; i++) { animals.Add(new Monkey()); }

            return new Zoo(animals);
        }

        public void ProcessIntervalElapsed()
        {
            foreach (var animal in _animals)
            {
                int newHealthRandomPercentage = 10; //TODO: replace with randomiser, 0-20
                animal.IntervalElapsedActivities(newHealthRandomPercentage);
            }
        }

        public void Feed()
        {
            foreach (var animalType in _animals.GroupBy(a => a.Type))
            {
                int newFeedRandomPercentage = 10; //TODO: replace with randomiser, 10-25
                foreach (var animal in animalType)
                {
                    animal.Feed(newFeedRandomPercentage);
                }
            }
        }
    }
}