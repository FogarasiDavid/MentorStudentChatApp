using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MentorStudent.Infrastructure.Persistence;
using MentorStudent.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MentorStudent.Infrastructure.Background
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<NotificationWorker> _logger;

        public NotificationWorker(IServiceProvider serviceProvider, ILogger<NotificationWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Email Worker elindult.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await DoWorkAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Hiba történt a háttérfolyamatban.");
                }

                await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
            }
        }

        private async Task DoWorkAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                var usersWithUnread = await context.Notifications
                    .Where(n => !n.isRead)
                    .GroupBy(n => n.UserId)
                    .Select(g => new { UserId = g.Key, Count = g.Count() })
                    .ToListAsync();

                foreach (var item in usersWithUnread)
                {
                    var user = await context.Users.FindAsync(item.UserId);
                    if (user == null) continue;

                    try
                    {
                        await emailSender.SendEmailAsync(
                            user.Email.Value,
                            "Olvasatlan üzeneteid",
                            $"Szia! Jelenleg {item.Count} olvasatlan értesítésed van a platformon.");

                        _logger.LogInformation($"Email elküldve neki: {user.Id}");

                    }
                    catch
                    {
                        _logger.LogWarning($"Nem sikerült emailt küldeni: {user.Id}");
                    }
                }
            }
        }
    }
}