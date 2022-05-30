namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        void Send(string subject, string body, string destynationEmail);
    }
}
