using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using HomeBudget.Service.Model;

namespace HomeBudget.Test
{

    public class CommonTransactionsTest
    {

        private IEnumerable<Transaction>  InitData()
        {
             var transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Name = "wyplataAdam",
                    Amount = 10000,
                    AccountGroupId = 1,
                    SettlementPeriodId = 1
                },
                 new Transaction()
                {
                    Name = "wplata na oszcz",
                    Amount = -100,
                    AccountGroupId = 1,
                    SettlementPeriodId = 1
                },
                 new Transaction()
                {
                    Name = "wplata na oszcz",
                    Amount = 100,
                    AccountGroupId = 2,
                    SettlementPeriodId = 1
                },
                 new Transaction()
                {
                    Name = "wyplata Ada",
                    Amount = 2000,
                    AccountGroupId = 2,
                    SettlementPeriodId = 2
                }
            };

            return transactions;
        }

        [Theory]
        [InlineData(0, 10000)]
        [InlineData(1, -100)]
        [InlineData(3, 2000)]
        public void HaveListOfAllTransactions(int elementNumber, decimal amount)
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            var El = commonTransactions.ListOfAllTransactions.ElementAt(elementNumber).Amount;
            Xunit.Assert.Equal(amount, El);
        }

        [Fact]
        public void HaveGoodLenghListOfAllTran()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(4, commonTransactions.ListOfAllTransactions.Count());
        }

        [Theory]
        [InlineData(0, 10000)]
        [InlineData(1, 100)]
        [InlineData(2, 2000)]
        public void HaveListOfIncome(int elementNumber, decimal amount)
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            var El = commonTransactions.ListOfIncome.ElementAt(elementNumber).Amount;
            Xunit.Assert.Equal(amount, El);
        }

        [Fact]
        public void HaveGoodLenghtListOfIncome()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(3, commonTransactions.ListOfIncome.Count());
        }

        [Theory]
        [InlineData(0, -100)]
        public void HaveListOfOutgoes(int elementNumber, decimal amount)
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            var El = commonTransactions.ListOfOutgoes.ElementAt(elementNumber).Amount;
            Xunit.Assert.Equal(amount, El);
        }

        [Fact]
        public void HaveGoodLenghtListOfOutgoes()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(1, commonTransactions.ListOfOutgoes.Count());
        }

        [Fact]
        public void GoodSumOfIncome()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(12100, commonTransactions.SumOfIncome);
        }

        [Fact]
        public void GoodSumOfoutgoes()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(-100, commonTransactions.SumOfOutgoes);
        }

        [Fact]
        public void GoodTransactionsDifferent()
        {
            ICommonTransactions commonTransactions = new CommonTransactions(InitData());
            Xunit.Assert.Equal(12000, commonTransactions.TransactionsDifferent);
        }
    }
}
