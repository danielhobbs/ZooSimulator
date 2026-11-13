namespace ZooSimulator.Api.Domain
{
    public class Zoo
    {
        public const int AnimalsPerType = 1;

        // TODO replace with values from a random source: 0-20 per animal, 10-25 per animal type
        private const int FixedHealthReductionPercent = 10;
        private const int FixedFeedPercent = 10;

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

        public void IntervalElapsed()
        {
            throw new NotImplementedException();
        }

        public void Feed()
        {
            throw new NotImplementedException();
        }
    }
}