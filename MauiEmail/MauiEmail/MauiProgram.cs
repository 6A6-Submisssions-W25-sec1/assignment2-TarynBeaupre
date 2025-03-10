using EmailConsoleApp.Interfaces;
using MailKit.Security;
using MauiEmail.Models;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace MauiEmail
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

           
            return builder.Build();
        }

    }
}
