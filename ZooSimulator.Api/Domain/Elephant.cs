namespace ZooSimulator.Api.Domain
{
    public class Elephant : Animal
    {
        public Elephant() 
        {
            Type = AnimalType.Elephant;
            Health = 100;
            CriticalThreshold = 70;
        }

        public override bool IsDead()
        {
            return Health < CriticalThreshold && Status == AnimalStatus.CannotWalk;
        }


        public bool CannotWalk()
        {
            return Health < CriticalThreshold && Status == AnimalStatus.Alive;
        }
    }
}
