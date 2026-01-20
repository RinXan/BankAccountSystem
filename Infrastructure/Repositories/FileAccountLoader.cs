using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Repositories;

namespace BankAccountSystem.Infrastructure.Repositories
{
    public class FileAccountLoader : IAccountLoader
    {
        private readonly string _filePath;
        public FileAccountLoader(string filePath) 
        {
            _filePath = filePath ?? throw new InvalidOperationException();
        }

        public IEnumerable<string> Load()
        {
            List<string> data = new List<string>();

            foreach (string line in File.ReadAllLines(_filePath))
            {
                data.Add(line);
            }

            return data;
        }
    }
}
