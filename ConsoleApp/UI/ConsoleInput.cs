using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountSystem.ConsoleApp.UI
{
    public class ConsoleInput
    {
        public static decimal ReadDecimal(string message)
        {
            Console.Write(message);
            return decimal.Parse(Console.ReadLine());
        }

        public static int ReadInt(string message)
        {
            Console.Write(message);
            return int.Parse(Console.ReadLine());
        }
    }
}
