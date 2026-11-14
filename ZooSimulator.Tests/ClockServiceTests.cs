using ZooSimulator.Api.Domain;
using ZooSimulator.Api.Services;

namespace ZooSimulator.Tests
{
    public class ClockServiceTests
    {
        [Fact]
        public async Task GivenRunningClock_WhenIntervalsPass_ThenZooIsToldAnIntervalElapsedEachTime()
        {
            // Arrange
            var zooService = new CountingZooService();
            var clockService = new ClockService(zooService, TimeSpan.FromMilliseconds(20));

            // Act
            await clockService.StartAsync(CancellationToken.None);
            await WaitUntil(() => zooService.IntervalElapsedCalls >= 2);
            await clockService.StopAsync(CancellationToken.None);

            // Assert
            Assert.True(zooService.IntervalElapsedCalls >= 2);
        }

        [Fact]
        public async Task GivenStoppedClock_WhenMoreTimePasses_ThenZooIsNotToldAgain()
        {
            // Arrange
            var zooService = new CountingZooService();
            var clockService = new ClockService(zooService, TimeSpan.FromMilliseconds(20));

            await clockService.StartAsync(CancellationToken.None);
            await WaitUntil(() => zooService.IntervalElapsedCalls >= 1);
            await clockService.StopAsync(CancellationToken.None);
            
            var callsWhenStopped = zooService.IntervalElapsedCalls;

            // Act
            await Task.Delay(100);

            // Assert
            Assert.Equal(callsWhenStopped, zooService.IntervalElapsedCalls);
        }

        private static async Task WaitUntil(Func<bool> condition)
        {
            var giveUpAt = DateTime.UtcNow.AddSeconds(5);

            while (!condition() && DateTime.UtcNow < giveUpAt)
            {
                await Task.Delay(10);
            }
        }

        private class CountingZooService : IZooService
        {
            private int _intervalElapsedCalls;

            public int IntervalElapsedCalls => _intervalElapsedCalls;

            public void ProcessInterval() => Interlocked.Increment(ref _intervalElapsedCalls);

            public Zoo GetZoo() => throw new NotImplementedException();
            public void FeedAnimals() => throw new NotImplementedException();
        }
    }
}
