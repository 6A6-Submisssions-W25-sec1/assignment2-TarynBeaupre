
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEmail.Models
{
    internal class ObservableMessage : INotifyPropertyChanged
    {
        public MailKit.UniqueId UniqueId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Subject { get; set; }
        public string? Body { get; set; }
        public string? HtmlBody { get; set; }
        public MimeKit.MailboxAddress From { get; set; }
        public List<MimeKit.MailboxAddress> To { get; set; }
        public bool IsRead { get; set; }
        public bool IsFavorite { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableMessage(IMessageSummary message)
        {
            UniqueId = message.UniqueId;
            Date = message.Date;
            Subject = message.Envelope.Subject;
            Body = null;
            HtmlBody = null;
            From = (MailboxAddress)message.Envelope.From[0];
            To = (List<MailboxAddress>)message.Envelope.To.Mailboxes;
            IsRead = (message.Flags == MessageFlags.Seen);
            IsFavorite = (message.Flags == MessageFlags.Flagged);
        }

        public ObservableMessage(MimeMessage mimeMessage, UniqueId uniqueId)
        {
            UniqueId = uniqueId;
            Date = mimeMessage.Date;
            Subject = mimeMessage.Subject;
            Body = mimeMessage.Body.ToString();
            HtmlBody = mimeMessage.HtmlBody;
            From = mimeMessage.From.Mailboxes.First();
            To = mimeMessage.To.Mailboxes;
            IsRead = mimeMessage;
            IsFavorite = mimeMessage;
        }

        public MimeMessage ToMime(ObservableMessage observableMessage)
        {

        }
    }
}
