﻿using ComicBookStoreAPI.Domain.Models;

namespace ComicBookStoreAPI.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
