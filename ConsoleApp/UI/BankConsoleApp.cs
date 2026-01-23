using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.ConsoleApp.UI
{
    public class BankConsoleApp
    {
        private readonly ConsoleMenu _menu;

        public BankConsoleApp(ConsoleMenu menu)
        {
            _menu = menu;
        }

        public void Run()
        {
            _menu.Show();
        }
    }
}
