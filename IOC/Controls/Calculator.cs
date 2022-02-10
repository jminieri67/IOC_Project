using IOC.Controls.Interfaces;
using IOC.Interfaces;

namespace IOC.Controls
{
	public class Calculator : ICalculator
	{
        private readonly ILogger _logger;

        public Calculator(ILogger logger)
        {
            _logger = logger;
        }

        public int Add(int num1, int num2)
        {
            int num3 = num1 + num2;

            _logger.WriteInfo($"{num1} + {num2} = {num3}");

            return num3;
        }
    }
}
