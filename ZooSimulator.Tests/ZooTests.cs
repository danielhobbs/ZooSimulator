using ZooSimulator.Api.Domain;

namespace ZooSimulator.Tests
{
    public class ZooTests
    {
        /* Tests TODO

        Animals reduce health indiviually
            - random range tests
        Animals fed at same percentage ... but is still based on their individual health

        Clock test: IntervalElapsed is triggered by a "tick" of the clock
         */

        private const float Tolerance = 0.001f;

        // Zoo set up

        [Fact]
        public void GivenDefaultZoo_WhenCreated_ThenHasFiveOfEachAnimalType()
        {
            // Arrange and Act
            var zoo = Zoo.CreateDefault();

            // Assert
            Assert.Equal(5, zoo.Animals.Count(animal => animal.Type == AnimalType.Elephant));
            Assert.Equal(5, zoo.Animals.Count(animal => animal.Type == AnimalType.Giraffe));
            Assert.Equal(5, zoo.Animals.Count(animal => animal.Type == AnimalType.Monkey));
            Assert.Equal(15, zoo.Animals.Count);
        }

        [Fact]
        public void GivenDefaultZoo_WhenCreated_ThenAllAnimalsHave100HealthPercent()
        {
            // Arrange and Act
            var zoo = Zoo.CreateDefault();

            // Assert
            Assert.All(zoo.Animals, animal => Assert.Equal(100f, animal.HealthPercent));
        }

        [Fact]
        public void GivenDefaultZoo_WhenCreated_ThenAllAnimalsAreAlive()
        {
            // Arrange and Act
            var zoo = Zoo.CreateDefault();

            // Assert
            Assert.All(zoo.Animals, animal => Assert.Equal(AnimalStatus.Alive, animal.Status));
        }

        // Interval

        [Fact]
        public void GivenZoo_WhenIntervalElapses_ThenEveryAnimalHasHealthPercentReduced()
        {
            // Arrange
            var zoo = new Zoo(new Animal[] { new Monkey(), new Giraffe(), new Elephant() });

            // Act
            zoo.ProcessIntervalElapsed();

            // Assert - fixed 10% for now: 100 - 10% = 90
            Assert.All(zoo.Animals, animal => Assert.Equal(90f, animal.HealthPercent, Tolerance));
        }

        // Feeding

        [Fact]
        public void GivenAnimalsAtDifferentHealth_WhenFed_ThenEachIncreasesByPercentageOfItsOwnHealth()
        {
            // Arrange
            var firstMonkey = new Monkey() { HealthPercent = 50f };
            var secondMonkey = new Monkey() { HealthPercent = 80f };
            var zoo = new Zoo(new Animal[] { firstMonkey, secondMonkey });

            // Act
            zoo.Feed();

            // Assert - fixed 10% for now: 50 + 10% = 55, 80 + 10% = 88
            Assert.Equal(55f, firstMonkey.HealthPercent, Tolerance);
            Assert.Equal(88f, secondMonkey.HealthPercent, Tolerance);
        }
    }
}

