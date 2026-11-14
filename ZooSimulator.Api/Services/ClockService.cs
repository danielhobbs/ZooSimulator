namespace ZooSimulator.Api.Services
{
    public class ClockService : BackgroundService
    {
        private readonly IZooService _zooService;
        private readonly TimeSpan _interval;

        public ClockService(IZooService zooService, TimeSpan interval)
        {
            _zooService = zooService;
            _interval = interval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(_interval);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _zooService.ProcessInterval();
            }
        }
    }
}