using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.ConsoleApp.UI;
using BankAccountSystem.Domain.Exceptions;
using BankAccountSystem.Domain.Services;

namespace BankAccountSystem.ConsoleApp.Controllers
{
    public class TransferController
    {
        private readonly TransferService _transferService;

        public TransferController(TransferService transferService)
        {
            _transferService = transferService;
        }
        public void Transfer()
        {
            try
            {
                int from = ConsoleInput.ReadInt("From account ID: ");
                int to = ConsoleInput.ReadInt("To account ID: ");
                decimal amount = ConsoleInput.ReadDecimal("Amount: ");

                _transferService.Transfer(from, to, amount);

                Console.WriteLine("Transfer completed");
            }
            catch (DomainException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
