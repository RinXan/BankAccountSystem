using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.Domain.Logger
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }
}
