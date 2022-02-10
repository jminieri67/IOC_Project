using IOC.Controls.Interfaces;
using System;
using System.Diagnostics;

namespace IOC.Controls
{
	public class Logger : ILogger
    {
        public Logger()
		{
		}

        public void WriteError(Exception ex, string s)
        {
            Trace.WriteLine($"{s}: Exception: {ex.Message}");
            Console.WriteLine($"{s}: Exception: {ex.Message}");
        }

        public void WriteInfo(string s)
        {
            Trace.WriteLine(s);
            Console.WriteLine(s);
        }
    }
}
