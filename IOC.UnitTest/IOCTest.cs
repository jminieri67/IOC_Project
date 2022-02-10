using IOC.Controls;
using IOC.Controls.Interfaces;
using IOC.UnitTest.TestClasses;
using IOC.UnitTest.TestClasses.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IOC.UnitTest
{

	[TestClass]
	public class IOCTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			Container container = new Container("Container");
			container.Register<ILogger, Logger>(LifecycleType.Singleton);
			container.Register<ICalculator, Calculator>();
			Calculator calculator = (Calculator)container.Resolve<ICalculator>();

			int result = calculator.Add(4, 89);

			Assert.AreEqual(93, result);
		}

		[TestMethod]
		public void TestMethod2()
		{
			Container container = new Container("Container");
			container.Register<ILogger, Logger>(LifecycleType.Singleton);

			Assert.ThrowsException<Exception>(() => (EmailService)container.Resolve<IEmailService>());
		}

		[TestMethod]
		public void TestMethod3()
		{
			Container container = new Container("Container");

			container.Register<ILogger, Logger>(LifecycleType.Singleton);
			container.Register<IUsersController, UsersController>();
			container.Register<ICalculator, Calculator>();
			container.Register<IEmailService, EmailService>();

			UsersController usersController = (UsersController)container.Resolve<IUsersController>();

			Assert.IsNotNull(usersController);
			Assert.IsNotNull(usersController.Calculator);
			Assert.IsNotNull(usersController.EmailService);
			Assert.IsNotNull(usersController.Logger);
		}
	}
}
