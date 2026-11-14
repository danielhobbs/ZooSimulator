using ZooSimulator.Api.Helpers;

namespace ZooSimulator.Tests
{
    public class RandomiserTests
    {
        [Fact]
        public void GivenSameMinAndMax_WhenNumberDrawn_ThenThatNumberIsReturned()
        {
            // Arrange
            var randomiser = new Randomiser(10, 10);

            // Act
            var number = randomiser.GetRandomNumber();

            // Assert
            Assert.Equal(10, number);
        }

        [Fact]
        public void GivenRange_WhenManyNumbersDrawn_ThenAllAreInRangeAndBothEndsAppear()
        {
            // Arrange
            var randomiser = new Randomiser(0, 20);

            // Act
            var numbers = Enumerable.Range(0, 1000).Select(i => randomiser.GetRandomNumber()).ToList();

            // Assert
            Assert.All(numbers, number => Assert.InRange(number, 0, 20));
            Assert.Contains(0, numbers);
            Assert.Contains(20, numbers);
        }
    }
}