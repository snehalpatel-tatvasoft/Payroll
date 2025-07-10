using PalladiumPayroll.Helper.Constants;
using PalladiumPayroll.Services.User;

namespace PalladiumPayroll.Job
{
    public class InactivityCheckerHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InactivityCheckerHostedService> _logger;

        public InactivityCheckerHostedService(IServiceProvider serviceProvider, ILogger<InactivityCheckerHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope? scope = _serviceProvider.CreateScope())
                {
                    IUserService? userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                    try
                    {
                        await userService.LogoutInactiveUsers();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, message: "Error while logging out inactive users.");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes((double)AppEnums.PositiveNumbers.One), stoppingToken);
            }
        }
    }
}
