using Assignment.Application.DTOs;

namespace Assignment.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequestDto request);
    }
}
