namespace MauiEmail
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            //// define new config
            //IMailConfig config = new MailConfig
            //{
            //    EmailAddress = "test90testy@gmail.com",
            //    Password = "vbtn cdck xxos bfyp", // can replace with an OAuth token later?
            //    ReceiveHost = "imap.gmail.com",
            //    ReceivePort = 993,
            //    ReceiveSocketOptions = SecureSocketOptions.SslOnConnect,
            //    SendHost = "smtp.gmail.com",
            //    SendPort = 587,
            //    SendSocketOptions = SecureSocketOptions.StartTls
            //};

            //IEmailService emailService = new EmailService(config);

            //// Testing sending
            //await emailService.StartSendClientAsync();
            //MimeMessage message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Test", config.EmailAddress));
            //message.To.Add(new MailboxAddress("Taryn", config.EmailAddress));
            //message.Subject = "Test Email";
            //message.Body = new TextPart("plain") { Text = "This is just a test!" };
            //await emailService.SendMessageAsync(message);
            //await emailService.StartSendClientAsync();
            //await emailService.DisconnectSendClientAsync();

            //// Testing email retrieving
            //await emailService.StartRetrieveClientAsync();
            //var emails = await emailService.DownloadAllEmailsAsync();

            //// Testing download/delete
            //if (emails.Any())
            //{
            //    Console.WriteLine("\nEnter an email index to delete.");

            //    if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < emails.Count())
            //    {
            //        await emailService.DeleteMessageAsync(index);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid index.");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No emails found.");
            //}

            //await emailService.DisconnectRetrieveClientAsync();

        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
