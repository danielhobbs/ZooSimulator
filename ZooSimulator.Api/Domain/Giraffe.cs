namespace ZooSimulator.Api.Domain
{
    public class Giraffe : Animal
    {
        public Giraffe() 
        {
            Type = AnimalType.Giraffe;
            HealthPercent = 100;
            CriticalThreshold = 50;
        }
    }
}
