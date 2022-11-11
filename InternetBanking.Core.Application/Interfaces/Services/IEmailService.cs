﻿using InternetBanking.Core.Application.Dtos.Email;
using InternetBanking.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public MailSettings _mailSettings { get; }
        Task SendEmailAsync(EmailRequest request);
    }
}
