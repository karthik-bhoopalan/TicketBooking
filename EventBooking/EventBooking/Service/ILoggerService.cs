using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBooking.Service
{
    public interface ILoggerService
    {
        void LogInformation(string message, params object[] parameters);
    }
}
