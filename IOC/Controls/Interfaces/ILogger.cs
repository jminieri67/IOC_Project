using System;

namespace IOC.Controls.Interfaces
{
    public interface ILogger
    {
        void WriteError(Exception ex, string s);
        void WriteInfo(string s);
    }
}
