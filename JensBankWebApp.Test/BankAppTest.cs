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
    }
}
