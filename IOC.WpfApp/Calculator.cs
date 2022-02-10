using IOC.Interfaces;
using IOC.WpfApp.Interfaces;

namespace IOC.WpfApp
{
    internal class Calculator : ICalculator
    {
        private readonly ILogger _logger;

        public Calculator(ILogger logger)
        {
            _logger = logger;
        }

        public void Add(int num1, int num2)
        {
            int num3 = num1 + num2;

            _logger.WriteInfo($"{num1} + {num2} = {num3}");
        }
    }
}
