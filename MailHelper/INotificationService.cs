using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MailHelper
{
    public interface INotificationService
    {
        Task<bool> SendMailNotification(List<string> toEmailIds, string data, string batchId);
        Task<bool> SendMailWithAttachment(List<string> toEmailIds, string category, string property,List<IFormFile> attachments);

    }
}
