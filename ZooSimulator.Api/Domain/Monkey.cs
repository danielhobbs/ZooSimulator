namespace ZooSimulator.Api.Domain
{
    public class Monkey : Animal
    {
        public Monkey() 
        {
            Type = AnimalType.Monkey;
            Health = 100;
            CriticalThreshold = 30;
        }
    }
}
