using System.Net;
using System.Net.Mail;

namespace ZagrebEvents.Web.Services
{
    // Slanje email obavijesti (potvrde rezervacija).
    // Ako SMTP nije konfiguriran (Smtp:Host u user-secrets/appsettings),
    // mail se sprema kao .html u App_Data/emails — vidljivo za dev/demo bez SMTP racuna.
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, IWebHostEnvironment env, ILogger<EmailService> logger)
        {
            _config = config;
            _env = env;
            _logger = logger;
        }

        public async Task SendAsync(string to, string subject, string htmlBody)
        {
            if (string.IsNullOrWhiteSpace(to)) return;

            var host = _config["Smtp:Host"];
            try
            {
                if (!string.IsNullOrWhiteSpace(host))
                {
                    using var client = new SmtpClient(host, int.TryParse(_config["Smtp:Port"], out var p) ? p : 587)
                    {
                        EnableSsl = true,
                        Credentials = new NetworkCredential(_config["Smtp:User"], _config["Smtp:Pass"])
                    };
                    var from = _config["Smtp:From"] ?? _config["Smtp:User"] ?? "noreply@gdjecemo.local";
                    using var msg = new MailMessage(from, to, subject, htmlBody) { IsBodyHtml = true };
                    await client.SendMailAsync(msg);
                    _logger.LogInformation("Email poslan na {To}: {Subject}", to, subject);
                }
                else
                {
                    // Dev fallback: spremi mail na disk
                    var dir = Path.Combine(_env.ContentRootPath, "App_Data", "emails");
                    Directory.CreateDirectory(dir);
                    var file = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd-HHmmss}-{Guid.NewGuid():N}.html");
                    var content = $"<!-- To: {to} -->\n<!-- Subject: {subject} -->\n{htmlBody}";
                    await File.WriteAllTextAsync(file, content);
                    _logger.LogInformation("SMTP nije konfiguriran - email za {To} spremljen u {File}", to, file);
                }
            }
            catch (Exception ex)
            {
                // Email ne smije srusiti rezervaciju
                _logger.LogError(ex, "Slanje emaila na {To} nije uspjelo", to);
            }
        }

        // Zajednicki dark-theme template
        public static string Wrap(string title, string inner) => $@"
<div style='background:#0a0a0f;padding:32px;font-family:Arial,sans-serif'>
  <div style='max-width:520px;margin:0 auto;background:#12121a;border:1px solid #3b2a63;border-radius:14px;padding:28px'>
    <h1 style='color:#a78bfa;font-size:1.3rem;margin:0 0 4px'>GdjeCemo</h1>
    <h2 style='color:#f1f5f9;font-size:1.1rem;margin:0 0 16px'>{title}</h2>
    <div style='color:#cbd5e1;font-size:0.95rem;line-height:1.6'>{inner}</div>
    <p style='color:#64748b;font-size:0.78rem;margin-top:20px'>Ovo je automatska poruka aplikacije GdjeCemo.</p>
  </div>
</div>";
    }
}
