using System;

namespace IOC.Interfaces
{
    public interface ILogger
    {
        void WriteError(Exception ex, string s);
        void WriteInfo(string s);
    }
}
