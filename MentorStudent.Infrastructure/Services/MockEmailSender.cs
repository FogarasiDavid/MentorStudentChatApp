using MentorStudent.Application.Interfaces;
using System.Threading.Tasks;

namespace MentorStudent.Infrastructure.Services
{
    public class MockEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string to, string subject, string body)
        {
           
            Console.WriteLine($"[Email küldése szimulálva] Címzett: {to}, Tárgy: {subject}");

            return Task.CompletedTask;
        }
    }
}