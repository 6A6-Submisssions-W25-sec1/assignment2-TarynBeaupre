using EmailConsoleApp.Interfaces;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMailConfig _config;
        private SmtpClient _stmpClient;
        private ImapClient _imapClient;

        // creating an instance of our mail configs, stmp and imap clients.
        public EmailService(IMailConfig config)
        {
            _config = config;
            _stmpClient = new SmtpClient();
            _imapClient = new ImapClient();
        }

        #region STMP
        public async Task StartSendClientAsync()
        {
            try
            {
                // connect stmp client is not connected
                if (!_stmpClient.IsConnected)
                {
                    await _stmpClient.ConnectAsync(_config.SendHost, _config.SendPort, _config.SendSocketOptions);
                }

                // authenticate client if not authenticated
                if (!_stmpClient.IsAuthenticated)
                {
                    await _stmpClient.AuthenticateAsync(_config.EmailAddress, _config.Password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send client could not be started. Exception: " + ex.Message);
            }
        }

        public async Task DisconnectSendClientAsync()
        {
            try
            {
                if (_stmpClient.IsConnected)
                    await _stmpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Send client could not be disconnected. Exception: " + ex.Message);
            }

        }
        #endregion

        #region IMAP
        public async Task StartRetrieveClientAsync()
        {
            try
            {
                // connect IMAP if not connected
                if (!_imapClient.IsConnected)
                    await _imapClient.ConnectAsync(_config.ReceiveHost, _config.ReceivePort, _config.ReceiveSocketOptions);
                // authenticate IMAP if not authenticated
                if (!_imapClient.IsAuthenticated)
                    await _imapClient.AuthenticateAsync(_config.EmailAddress, _config.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Retrieve client could not be started. Exception: " + ex.Message);
            }
        }

        public async Task DisconnectRetrieveClientAsync()
        {
            try
            {
                // disconnect IMAP if connected
                if (_imapClient.IsConnected)
                    await _imapClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Retrieve client could not be disconnected. Exception: " + ex.Message);
            }
        }
        #endregion

        #region Mail Options
        public async Task SendMessageAsync(MimeMessage message)
        {
            try
            {
                await StartSendClientAsync();
                await _stmpClient.SendAsync(message);
                Console.WriteLine("Message successfully sent with subject: " + message.Subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message could not be sent. Exception: " + ex.Message);
            }
            finally
            {
                await DisconnectSendClientAsync();
            }
        }
        public async Task<IEnumerable<MimeMessage>> DownloadAllEmailsAsync()
        {
            var emails = new List<MimeMessage>();

            try
            {
                // start the retrieve client, get all emails and store in inbox variable
                await StartRetrieveClientAsync();
                var inbox = _imapClient.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                // printing all messages to console
                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = await inbox.GetMessageAsync(i);
                    emails.Add(message);
                    Console.WriteLine($"Email index {i}: {message.Subject} - {message.Date}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Emails could not be downloaded. Exception: " + ex.Message);
            }
            finally
            {
                await DisconnectRetrieveClientAsync();
            }
            return emails;
        }

        public async Task DeleteMessageAsync(int uid)
        {
            try
            {
                // start retrieve client, open inbox with read write access
                await StartRetrieveClientAsync();
                var inbox = _imapClient.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadWrite);

                var message = inbox.GetMessage(uid);
                inbox.AddFlags(uid, MessageFlags.Deleted, true);
                await inbox.ExpungeAsync();
                Console.WriteLine($"Email with id {uid} deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email could not be deleted. Exception:" + ex.Message);
            }
            finally
            {
                await DisconnectRetrieveClientAsync();
            }
        }
        #endregion
    }
}
