using IOC.Interfaces;
using IOC.UnitTest.TestClasses;
using IOC.UnitTest.TestClasses.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IOC.UnitTest
{

	[TestClass]
	public class IOCTest
	{
		private Container _testContainer;

		public IOCTest()
		{
			_testContainer = new Container("TestContainer");
			_testContainer.Register<ILogger, Logger>(LifecycleType.Singleton);
			_testContainer.Register<IMyTestClassA, MyTestClassA>();
			_testContainer.Register<IMyTestClassB, MyTestClassB>();
		}


		[TestMethod]
		public void TestMethod1()
		{
			MyTestClassA myTestClassA = (MyTestClassA)_testContainer.Resolve<IMyTestClassA>();

			bool didSomething = myTestClassA.DoSomething();

			Assert.IsTrue(didSomething);
		}

		[TestMethod]
		public void TestMethod2()
		{
			Assert.ThrowsException<Exception>(() => (MyTestClassC)_testContainer.Resolve<IMyTestClassC>());
		}
	}
}
