using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountSystem.Domain.Accounts;
using BankAccountSystem.Domain.Exceptions;
using Xunit;

namespace BankAccountSystem.Tests.Domain.Accounts
{
    public class BankAccountWithdrawTests
    {
        [Fact]
        public void Withdraw_WhenEnoughMoney_DecreasesBalance()
        {
            // Arrange - Подготовка
            BankAccount account = new SavingsAccount(1, "Olga", 100);
            account.Deposit(800);

            // Act - Действие
            account.Withdraw(400);

            // Assert - Проверка
            Assert.Equal(400, account.Balance);
        }

        [Fact]
        public void Withdraw_WhenNotEnoughMoney_ThrowsNotEnoughMoneyException()
        {
            // Arrange
            BankAccount account = new SavingsAccount(1, "Olesya", 100);
            account.Deposit(90);

            // Act & Assert
            Assert.Throws<NotEnoughMoneyException>(() => account.Withdraw(100));
        }

        [Fact]
        public void Withdraw_WhenAmountIsZero_ThrowsInvalidTransferAmountException()
        {
            // Arrange
            BankAccount account = new SavingsAccount(1, "Any", 100);
            account.Deposit(1000);

            // Act & Assert
            Assert.Throws<InvalidTransferAmountException>(() => account.Withdraw(0));
        }

        [Fact]
        public void Withdraw_WhenAmountIsNegative_ThrowsInvalidTransferAmountException()
        {
            // Arrange
            BankAccount account = new SavingsAccount(1, "Any", 100);
            account.Deposit(1000);

            // Act & Assert
            Assert.Throws<InvalidTransferAmountException>(() => account.Withdraw(-45));
        }

    }
}
