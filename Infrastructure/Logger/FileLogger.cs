using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Logger;

namespace BankAccountSystem.Infrastructure.Logger
{
    public class FileLogger(string filePath) : ILogger
    {
        private readonly string _filePath = string.IsNullOrWhiteSpace(filePath) ? throw new ArgumentException("File path is not correct") : filePath;
        public void Log(LogLevel level, string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            File.AppendAllText(_filePath, line + Environment.NewLine);
        }
    }
}
