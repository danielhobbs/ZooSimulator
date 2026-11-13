namespace ZooSimulator.Api.Domain
{
    public class Monkey : Animal
    {
        public Monkey() 
        {
            Type = AnimalType.Monkey;
            HealthPercent = 100;
            CriticalThreshold = 30;
        }
    }
}
