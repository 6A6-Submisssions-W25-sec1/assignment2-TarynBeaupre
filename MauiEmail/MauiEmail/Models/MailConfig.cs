using EmailConsoleApp.Interfaces;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail.Models
{
    public class MailConfig : IMailConfig
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }


        // using google defaults
        public string ReceiveHost { get; set; } = "imap.gmail.com";

        public SecureSocketOptions ReceiveSocketOptions { get; set; } = SecureSocketOptions.SslOnConnect;

        public int ReceivePort { get; set; } = 993;


        public string SendHost { get; set; } = "stmp.gmail.com";

        public int SendPort { get; set; } = 587;

        public SecureSocketOptions SendSocketOptions { get; set; } = SecureSocketOptions.StartTls;


        public string OAuth2ClientId { get; set; } = "";

        public string OAuth2ClientSecret { get; set; } = "";

        public string OAuth2RefreshToken { get; set; } = "";
    }
}
