using ZooSimulator.Api.Services;

namespace ZooSimulator.Api.Domain
{
    public class Zoo
    {
        public List<Animal> Animals { get; set; } = new List<Animal>();
        public Clock Clock { get; }

        private readonly IClockService _clockService = new ClockService();
                
        public void AddAnimal(Animal animal)
        {
            Animals.Add(animal);
        }

        public Zoo(List<Animal> animals)
        {
            Animals = animals;
            Clock = _clockService.CreateClock();
        }
    }
}
