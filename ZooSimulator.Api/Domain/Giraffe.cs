namespace ZooSimulator.Api.Domain
{
    public class Giraffe : Animal
    {
        public Giraffe() 
        {
            Type = AnimalType.Giraffe;
            Health = 100;
            CriticalThreshold = 50;
        }
    }
}
