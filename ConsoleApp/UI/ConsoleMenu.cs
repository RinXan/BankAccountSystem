using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.ConsoleApp.Controllers;

namespace BankAccountSystem.ConsoleApp.UI
{
    public class ConsoleMenu
    {
        private readonly TransferController _controller;
        public ConsoleMenu(TransferController controller)
        {
            _controller = controller;
        }

        public void Show()
        {
            Console.WriteLine("1. Transfer money");
            Console.WriteLine("2. Exit");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _controller.Transfer();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
