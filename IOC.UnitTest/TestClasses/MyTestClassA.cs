using IOC.Interfaces;
using IOC.UnitTest.TestClasses.Interfaces;
using System.Reflection;

namespace IOC.UnitTest.TestClasses
{
	internal class MyTestClassA : IMyTestClassA
	{
		private readonly ILogger _logger;

		public MyTestClassA(ILogger logger)
		{
			_logger = logger;
		}
		public bool DoSomething()
		{
			bool didSomething = true;

			_logger.WriteInfo($"Call to {nameof(MyTestClassA)}.{MethodBase.GetCurrentMethod().Name}");

			return didSomething;
		}
	}
}
