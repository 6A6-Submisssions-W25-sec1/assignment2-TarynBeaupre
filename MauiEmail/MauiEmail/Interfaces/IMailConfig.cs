using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailConsoleApp.Interfaces
{
    public interface IMailConfig
    {
        // authentication
        string EmailAddress { get; }
        string Password { get; }

        // retrieving service
        string ReceiveHost { get; }
        SecureSocketOptions ReceiveSocketOptions { get; }
        int ReceivePort { get; }

        // sending service
        string SendHost { get; }
        int SendPort { get; }
        SecureSocketOptions SendSocketOptions { get; }

        // OAuth 2.0 optional props
        string OAuth2ClientId { get; }
        string OAuth2ClientSecret { get; }
        public string OAuth2RefreshToken { get; }
    }
}
