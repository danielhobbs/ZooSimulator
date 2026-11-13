namespace ZooSimulator.Api.Domain
{
    public class Elephant : Animal
    {
        public Elephant() 
        {
            Type = AnimalType.Elephant;
            HealthPercent = 100;
            CriticalThreshold = 70;
        }

        public override bool MeetsDeathCondition()
        {
            return HealthPercent < CriticalThreshold && Status == AnimalStatus.CannotWalk;
        }

        public bool MeetsCannotWalkCondition()
        {
            return HealthPercent < CriticalThreshold && (Status == AnimalStatus.Alive || Status == AnimalStatus.CannotWalk);
        }

        public override void AssessAfterInterval()
        {
            if (MeetsDeathCondition())
            {
                Status = AnimalStatus.Dead;
                return;
            }

            if (MeetsCannotWalkCondition())
            {
                Status = AnimalStatus.CannotWalk;
                return;
            }

            Status = AnimalStatus.Alive;
        }

        public override void AssessAfterFeed()
        {
            if (MeetsCannotWalkCondition())
            {
                Status = AnimalStatus.CannotWalk;
                return;
            }

            Status = AnimalStatus.Alive;
        }
    }
}
