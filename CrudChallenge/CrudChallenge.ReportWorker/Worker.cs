using CrudChallenge.Data.Repositories;
using CrudChallenge.Notifications.Services;

namespace CrudChallenge.ReportWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private IProductRepository _productRepository;

        private IEmailService _emailService;

        public Worker(ILogger<Worker> logger, IProductRepository productRepository, IEmailService emailService)
        {
            _logger = logger;
            _productRepository = productRepository;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var list = await _productRepository.GetProductsAsync();

            var report = "The number of products in our system is " + list.Count();

            _emailService.SendReportReadyEmail(report);
        }
    }
}