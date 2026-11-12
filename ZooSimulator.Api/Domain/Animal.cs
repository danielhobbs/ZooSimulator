namespace ZooSimulator.Api.Domain
{
    public abstract class Animal
    {
        public AnimalType Type { get; init; }
        public AnimalStatus Status { get; set; }
        public float Health { get; set; }
        public float CriticalThreshold { get; init; }

        public void IntervalElapsed(int percent)
        {
            ReduceHealth(percent);
            AssessAfterInterval();
        }

        public void Feed(int percent) 
        {
            Health += percent;
        }

        public void ReduceHealth(int percent)
        {
            Health -= percent;
        }

        public virtual bool IsDead()
        {
            return Health < CriticalThreshold;
        }

        public virtual void AssessAfterInterval()
        {
            if (IsDead())
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
