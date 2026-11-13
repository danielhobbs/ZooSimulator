namespace ZooSimulator.Api.Domain
{
    public abstract class Animal
    {
        public AnimalType Type { get; init; }
        // TODO Could make protected sets - would need unit tests updated and/or creating constructors
        public AnimalStatus Status { get; set; }
        public float HealthPercent { get; set; }
        public float CriticalThreshold { get; init; }

        public void IntervalElapsedActivities(int healthReductionPercent)
        {
            if (Status == AnimalStatus.Dead)
            {
                return;
            }

            ReduceHealth(healthReductionPercent);
            AssessAfterInterval();
        }

        public void Feed(int percent) 
        {
            if (Status == AnimalStatus.Dead)
            {
                return;
            }

            var newHealth = HealthPercent + (percent * HealthPercent / 100f);
            HealthPercent = newHealth > 100f ? 100f : newHealth;

            AssessAfterFeed();
        }

        private void ReduceHealth(int percent)
        {
            HealthPercent = HealthPercent - (percent * HealthPercent / 100f);
        }

        protected virtual bool MeetsDeathCondition()
        {
            return HealthPercent < CriticalThreshold;
        }

        protected virtual void AssessAfterInterval()
        {
            if (MeetsDeathCondition())
            {
                Status = AnimalStatus.Dead;
            }
        }

        protected virtual void AssessAfterFeed()
        { }
    }
}
