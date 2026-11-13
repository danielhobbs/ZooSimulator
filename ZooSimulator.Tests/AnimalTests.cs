namespace ZooSimulator.Tests
{
    public class AnimalTests
    {
        /* Tests TODO

        Dead animal stays dead
        Monkey and giraffe die imediately if health is below critical threshold
        Elephant cannot walk if health is below critical threshold and they are alive
        Elephant dies if already cannot walk and health is below critical threshold
        ReduceHelath reduces health by (supplied) percentage of current health
        Feed increases health by (supplied) percentage of current health, but not above 100
        Animal is Alive if health is above critical threshold 
            (Elephant can go from Cannot walk to Alive; but not from Dead)

        Health to 1dp

        IntervalElapsedActivities event triggers ReduceHealth for all animals in the zoo
        IntervalElapsedActivities event triggers AssessAfterInterval for all animals in the zoo
            - statuses as expected
         */

        [Fact]
        public void Test1()
        {

        }
    }
}