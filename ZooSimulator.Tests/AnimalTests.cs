using ZooSimulator.Api.Domain;

namespace ZooSimulator.Tests
{
    public class AnimalTests
    {
        private const float Tolerance = 0.001f;

        private static Animal CreateAnimal(AnimalType type, float healthPercent)
        {
            return type switch
            {
                AnimalType.Monkey => new Monkey() { HealthPercent = healthPercent },
                AnimalType.Giraffe => new Giraffe() { HealthPercent = healthPercent },
                AnimalType.Elephant => new Elephant() { HealthPercent = healthPercent },
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }

        /////////////////////////
        // Dead animal stays dead

        [Fact]
        public void GivenDeadAnimal_WhenFed_ThenStatusAndHealthPercentUnchanged()
        {
            // Arrange
            var startingHealth = 29f;
            var animal = new Monkey() { HealthPercent = startingHealth, Status = AnimalStatus.Dead };

            // Act
            animal.Feed(100);

            // Assert
            Assert.Equal(AnimalStatus.Dead, animal.Status);
            Assert.Equal(startingHealth, animal.HealthPercent);
        }

        [Fact]
        public void GivenDeadAnimal_WhenIntervalElapses_ThenStatusAndHealthPercentUnchanged()
        {
            // Arrange
            var startingHealth = 29f;
            var animal = new Monkey() { HealthPercent = startingHealth, Status = AnimalStatus.Dead };

            // Act
            animal.IntervalElapsedActivities(20);

            // Assert
            Assert.Equal(AnimalStatus.Dead, animal.Status);
            Assert.Equal(startingHealth, animal.HealthPercent);
        }


        /////////////////////////
        // Health maths

        [Fact]
        public void GivenAliveAnimal_WhenIntervalElapses_ThenHealthPercentReducedByPercentageOfCurrentHealth()
        {
            // Arrange
            var startingHealth = 50f;
            var reductionPercentage = 20;
            var animal = new Monkey() { HealthPercent = startingHealth, Status = AnimalStatus.Alive };

            // Act
            animal.IntervalElapsedActivities(reductionPercentage);

            // Assert
            var expectedHealth = 40f;

            Assert.Equal(expectedHealth, animal.HealthPercent, Tolerance);
        }

        [Fact]
        public void GivenAliveAnimal_WhenFed_ThenHealthPercentIncreasedByPercentageOfCurrentHealth()
        {
            // Arrange
            var startingHealth = 50f;
            var feedPercentage = 20;
            var animal = new Monkey() { HealthPercent = startingHealth, Status = AnimalStatus.Alive };

            // Act
            animal.Feed(feedPercentage);

            // Assert
            var expectedHealth = 60f;
            Assert.Equal(expectedHealth, animal.HealthPercent, Tolerance);
        }

        [Fact]
        public void GivenAliveAnimal_WhenFedBeyondFullHealth_ThenHealthPercentCappedAt100()
        {
            // Arrange
            var startingHealth = 90f;
            var feedPercentage = 25;
            var animal = new Monkey() { HealthPercent = startingHealth, Status = AnimalStatus.Alive };

            // Act
            animal.Feed(feedPercentage);

            // Assert
            var expectedHealth = 100f;
            Assert.Equal(expectedHealth, animal.HealthPercent, Tolerance);
        }


        /////////////////////////
        // Monkey and Giraffe
        
        [Theory]
        [InlineData(AnimalType.Monkey, 30f, 10)]   // 30 - 10% = 27, below 30
        [InlineData(AnimalType.Giraffe, 50f, 10)]  // 50 - 10% = 45, below 50
        public void GivenMonkeyOrGiraffe_WhenIntervalTakesHealthBelowThreshold_ThenDead(AnimalType type, float startingHealth, int reductionPercentage)
        {
            // Arrange
            var animal = CreateAnimal(type, startingHealth);

            // Act
            animal.IntervalElapsedActivities(reductionPercentage);

            // Assert
            Assert.Equal(AnimalStatus.Dead, animal.Status);
        }

        [Theory]
        [InlineData(AnimalType.Monkey, 37.5f, 20)]    // 37.5 - 20% = 30
        [InlineData(AnimalType.Giraffe, 62.5f, 20)]   // 62.5 - 20% = 50
        [InlineData(AnimalType.Elephant, 87.5f, 20)]  // 87.5 - 20% = 70
        public void GivenAliveAnimal_WhenIntervalLeavesHealthAtExactlyThreshold_ThenAlive(AnimalType type, float startingHealth, int reductionPercentage)
        {
            // Arrange
            var animal = CreateAnimal(type, startingHealth);

            // Act
            animal.IntervalElapsedActivities(reductionPercentage);

            // Assert
            Assert.Equal(AnimalStatus.Alive, animal.Status);
        }


        /////////////////////////
        // Elephant

        [Fact]
        public void GivenAliveElephant_WhenIntervalTakesHealthBelow70_ThenCannotWalk()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 75f, Status = AnimalStatus.Alive };

            // Act
            elephant.IntervalElapsedActivities(10); // 75 - 10% = 67.5

            // Assert
            Assert.Equal(AnimalStatus.CannotWalk, elephant.Status);
        }

        [Fact]
        public void GivenElephantThatCannotWalk_WhenNextIntervalLeavesHealthBelow70_ThenDead()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 65f, Status = AnimalStatus.CannotWalk };

            // Act
            elephant.IntervalElapsedActivities(10); // 65 - 10% = 58.5

            // Assert
            Assert.Equal(AnimalStatus.Dead, elephant.Status);
        }

        [Fact]
        public void GivenAliveElephant_WhenTwoIntervalsBothLeaveHealthBelow70_ThenCannotWalkThenDead()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 75f, Status = AnimalStatus.Alive };

            // Act and Assert
            elephant.IntervalElapsedActivities(10); // 67.5
            Assert.Equal(AnimalStatus.CannotWalk, elephant.Status);

            elephant.IntervalElapsedActivities(10); // 60.75
            Assert.Equal(AnimalStatus.Dead, elephant.Status);
        }

        [Fact]
        public void GivenElephantThatCannotWalk_WhenFedTo70OrAbove_ThenAlive()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 60f, Status = AnimalStatus.CannotWalk };

            // Act
            elephant.Feed(20); // 60 + 20% = 72

            // Assert
            Assert.Equal(AnimalStatus.Alive, elephant.Status);
        }

        [Fact]
        public void GivenElephantThatCannotWalk_WhenFedButStillBelow70_ThenStillCannotWalk()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 60f, Status = AnimalStatus.CannotWalk };

            // Act
            elephant.Feed(10); // 60 + 10% = 66

            // Assert
            Assert.Equal(AnimalStatus.CannotWalk, elephant.Status);
        }

        [Fact]
        public void GivenElephantRecoveredByFeeding_WhenNextIntervalLeavesHealthAt70OrAbove_ThenAlive()
        {
            // Arrange
            var elephant = new Elephant() { HealthPercent = 60f, Status = AnimalStatus.CannotWalk };
            elephant.Feed(25); // 60 + 25% = 75

            // Act
            elephant.IntervalElapsedActivities(5); // 75 - 5% = 71.25

            // Assert
            Assert.Equal(AnimalStatus.Alive, elephant.Status);
        }

        [Fact]
        public void GivenDeadElephant_WhenFedTo70OrAbove_ThenRemainsDead()
        {
            // Arrange
            var startingHealth = 60f;
            var elephant = new Elephant() { HealthPercent = startingHealth, Status = AnimalStatus.Dead };

            // Act
            elephant.Feed(25); // would be 75 if feeding were allowed

            // Assert
            Assert.Equal(AnimalStatus.Dead, elephant.Status);
            Assert.Equal(startingHealth, elephant.HealthPercent);
        }


    }
}