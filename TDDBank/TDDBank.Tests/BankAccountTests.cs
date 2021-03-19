using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TDDBank.Tests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void BankAccount_a_new_account_should_have_0_as_balance()
        {
            var ba = new BankAccount();

            Assert.AreEqual(0m, ba.Balance);
        }

        [TestMethod]
        public void BankAccount_can_deposit()
        {
            var ba = new BankAccount();

            ba.Deposit(5m);
            Assert.AreEqual(5m, ba.Balance);

            ba.Deposit(7m);
            Assert.AreEqual(12m, ba.Balance);
        }

        //[TestMethod]
        //public void BankAccount_deposit_a_negative_or_0_value_throws()
        //{
        //    var ba = new BankAccount();

        //    Assert.ThrowsException<ArgumentException>(() => ba.Deposit(0));
        //    Assert.ThrowsException<ArgumentException>(() => ba.Deposit(-1));
        //}

        [TestMethod]
        public void BankAccount_can_withdraw()
        {
            var ba = new BankAccount();
            ba.Deposit(12m);

            ba.Withdraw(4m);
            Assert.AreEqual(8m, ba.Balance);

            ba.Withdraw(3m);
            Assert.AreEqual(5m, ba.Balance);
        }

        [TestMethod]
        public void BankAccount_withdraw_a_negative_or_0_value_throws()
        {
            var ba = new BankAccount();

            Assert.ThrowsException<ArgumentException>(() => ba.Withdraw(0));
            Assert.ThrowsException<ArgumentException>(() => ba.Withdraw(-1));
        }

        [TestMethod]
        public void BankAccount_withdraw_below_0_throws()
        {
            var ba = new BankAccount();
            ba.Deposit(6m);

            Assert.ThrowsException<InvalidOperationException>(() => ba.Withdraw(8m));
        }

    }
}
