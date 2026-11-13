namespace ZooSimulator.Api.Domain
{
    public abstract class Animal
    {
        public AnimalType Type { get; init; }
        public AnimalStatus Status { get; set; }
        public float HealthPercent { get; set; }
        public float CriticalThreshold { get; init; }

        public void IntervalElapsedActivities(int healthReductionPercent)
        {
            ReduceHealth(healthReductionPercent);
            AssessAfterInterval();
        }

        public void Feed(int percent) 
        {
            // Could use private "IncreaseHealth" here if there was more to it
            HealthPercent += percent;
        }

        private void ReduceHealth(int percent)
        {
            HealthPercent -= percent;
        }

        public virtual bool MeetsDeathCondition()
        {
            return HealthPercent < CriticalThreshold;
        }

        public virtual void AssessAfterInterval()
        {
            if (MeetsDeathCondition())
            {
                SetStatus(AnimalStatus.Dead);
            }
        }

        private void SetStatus(AnimalStatus status)
        {
            Status = status;
        }
    }
}
