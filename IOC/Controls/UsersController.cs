using IOC.Controls.Interfaces;

namespace IOC.Controls
{
	public class UsersController : IUsersController
	{
		public ILogger Logger;
		public ICalculator Calculator;
		public IEmailService EmailService;

		public UsersController(
			ILogger logger, 
			ICalculator calculator, 
			IEmailService emailService)
		{
			Logger = logger;
			Calculator = calculator;
			EmailService = emailService;
		}
	}
}
