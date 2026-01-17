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

        [Theory]
        [InlineData(0)]
        [InlineData(-45)]
        public void Withdraw_WhenAmountIsZeroOrNegative_ThrowsInvalidTransferAmountException(decimal amount)
        {
            // Arrange
            BankAccount account = new SavingsAccount(1, "Any", 100);
            account.Deposit(1000);

            // Act & Assert
            Assert.Throws<InvalidTransferAmountException>(() => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(601)]
        [InlineData(800)]
        [InlineData(1000)]
        public void Withdraw_WhenCreditLimitExceeds_ThrowsNotEnoughMoneyException(decimal amount)
        {
            // Arrange
            BankAccount account = new CreditAccount(1, "Test", 500);
            account.Deposit(100);

            // Act & Assert
            Assert.Throws<NotEnoughMoneyException>(() => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(100)]
        [InlineData(300)]
        [InlineData(500)]
        public void Withdraw_WhenWithinCreditLimit_DecreasesBalance(decimal amount)
        {
            // Arrange
            BankAccount account = new CreditAccount(1, "test5", 500);
            account.Deposit(100);

            // Act
            account.Withdraw(amount);

            // Assert
            Assert.Equal(100 - amount, account.Balance);
        }
    }
}
