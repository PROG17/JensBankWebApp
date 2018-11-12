using JensBankWebApp.Models;
using System;
using Xunit;

namespace JensBankWebApp.Test
{
    public class BankAppTest
    {
        private Models.BankRepository _bankRepo = new Models.BankRepository();
        private decimal _testAmount = 100;

        [Fact]
        public void DepositWhenAccountNrIsCorrect()
        {
            var account = _bankRepo.GetAccountById(31234);
            var expected = account.Amount + _testAmount;

            var vm = new ViewModels.DepositWithdrawViewModel
            {
                AccountNo = account.Id,
                Amount = _testAmount
            };

            var actual = _bankRepo.Deposit(vm).Account.Amount;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DepositWhenAccountNrIsIncorrect()
        {
            var vm = new ViewModels.DepositWithdrawViewModel
            {
                AccountNo = 1234,
                Amount = _testAmount
            };

            vm = _bankRepo.Deposit(vm);

            Assert.True(vm.Account == null);
        }

        [Fact]
        public void WithdrawWithValidAmount()
        {
            var account = _bankRepo.GetAccountById(31234);
            var expected = account.Amount - _testAmount;

            var vm = new ViewModels.DepositWithdrawViewModel
            {
                AccountNo = account.Id,
                Amount = _testAmount
            };

            var actual = _bankRepo.Withdraw(vm).Account.Amount;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WithdrawWithInvalidAmount()
        {
            var account = _bankRepo.GetAccountById(11234);

            var vm = new ViewModels.DepositWithdrawViewModel
            {
                AccountNo = account.Id,
                Amount = 500
            };

            var result = _bankRepo.Withdraw(vm);

            Assert.True(result.Account == null);
        }

        [Fact]
        public void WithdrawWhenAccountNrIsIncorrect()
        {
            var vm = new ViewModels.DepositWithdrawViewModel
            {
                AccountNo = 1234,
                Amount = _testAmount
            };

            vm = _bankRepo.Deposit(vm);

            Assert.True(vm.Account == null);
        }

        [Fact]
        public void Can_transfer_money_when_origin_balance_has_coverage()
        {
            // ARRANGE
            var originAccount = _bankRepo.GetAccountById(11234);
            var destinationAccount = _bankRepo.GetAccountById(22345);

            var currentOriginBalance = originAccount.Amount;
            var currentDestinationBalance = destinationAccount.Amount;

            // ACT
            var amount = 150.0m;

            string msg = null;
            bool status = _bankRepo.MoneyTransfer(originAccount, destinationAccount, amount, out msg);

            // ASSERT
            var resultBalanceOrigin = originAccount.Amount;
            var resultBalanceDestination = destinationAccount.Amount;

            var expectedBalanceOrigin = currentOriginBalance - amount;
            var expectedBalanceDestination = currentDestinationBalance + amount;

            Assert.Equal(expectedBalanceOrigin, resultBalanceOrigin);
            Assert.Equal(expectedBalanceDestination, resultBalanceDestination);
            Assert.True(status);
        }


        [Fact]
        public void Cannot_transfer_money_when_origin_balance_has_NOT_coverage()
        {
            // ARRANGE
            var originAccount = _bankRepo.GetAccountById(11234);
            var destinationAccount = _bankRepo.GetAccountById(22345);

            var currentOriginBalance = originAccount.Amount;
            var currentDestinationBalance = destinationAccount.Amount;

            // ACT
            var amount = 700.0m;

            string msg = null;
            bool status = _bankRepo.MoneyTransfer(originAccount, destinationAccount, amount, out msg);

            // ASSERT
            var resultBalanceOrigin = originAccount.Amount;
            var resultBalanceDestination = destinationAccount.Amount;

            var expectedBalanceOrigin = currentOriginBalance;
            var expectedBalanceDestination = currentDestinationBalance;

            Assert.Equal(expectedBalanceOrigin, resultBalanceOrigin);
            Assert.Equal(expectedBalanceDestination, resultBalanceDestination);
            Assert.False(status);
        }

        [Fact]
        public void Cannot_transfer_money_if_amount_is_LessOrEqual_to_Zero()
        {
            // ARRANGE
            var originAccount = _bankRepo.GetAccountById(11234);
            var destinationAccount = _bankRepo.GetAccountById(22345);

            var currentOriginBalance = originAccount.Amount;
            var currentDestinationBalance = destinationAccount.Amount;

            // ACT
            var amount = -400.0m;

            string msg = null;
            bool status = _bankRepo.MoneyTransfer(originAccount, destinationAccount, amount, out msg);

            // ASSERT
            var resultBalanceOrigin = originAccount.Amount;
            var resultBalanceDestination = destinationAccount.Amount;

            var expectedBalanceOrigin = currentOriginBalance;
            var expectedBalanceDestination = currentDestinationBalance;

            Assert.Equal(expectedBalanceOrigin, resultBalanceOrigin);
            Assert.Equal(expectedBalanceDestination, resultBalanceDestination);
            Assert.False(status);
        }

        [Fact]
        public void Cannot_transfer_money_if_origin_account_IS_null()
        {
            // ARRANGE
            Account originAccount = null;
            var destinationAccount = _bankRepo.GetAccountById(22345);

            // ACT
            var amount = 30.0m;

            string msg = null;
            bool status = _bankRepo.MoneyTransfer(originAccount, destinationAccount, amount, out msg);

            // ASSERT

            Assert.False(status);
            Assert.Equal("Error: Invalid origin and/or destination account", msg);
        }

        [Fact]
        public void Cannot_transfer_money_if_destiantion_account_IS_null()
        {
            // ARRANGE
            var originAccount = _bankRepo.GetAccountById(22345);
            Account destinationAccount = null;

            // ACT
            var amount = 30.0m;

            string msg = null;
            bool status = _bankRepo.MoneyTransfer(originAccount, destinationAccount, amount, out msg);

            // ASSERT

            Assert.False(status);
            Assert.Equal("Error: Invalid origin and/or destination account", msg);
        }

    }
}
