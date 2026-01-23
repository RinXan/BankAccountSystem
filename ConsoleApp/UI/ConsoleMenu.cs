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
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("*** BANK SYSTEM ***");
                Console.WriteLine("1. Show all accounts");
                Console.WriteLine("2. Show account details");
                Console.WriteLine("3. Transfer money");
                Console.WriteLine("0. Exit");
                Console.Write("\nChoice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        _controller.ShowAllAccounts();
                        break;
                    case 2:
                        _controller.ShowAccountsDetails();
                        break;
                    case 3:
                        _controller.Transfer();
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
