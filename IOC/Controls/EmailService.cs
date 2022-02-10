using IOC.Controls.Interfaces;
using System.Diagnostics;

namespace IOC.Controls
{
	public class EmailService : IEmailService
	{
        private readonly ILogger _logger;

        public EmailService(ILogger logger)
        {
            _logger = logger;
        }

		public bool SendMail(string message)
		{
			Trace.WriteLine($"Email message sent: {message}");

			return true;
		}
	}
}
