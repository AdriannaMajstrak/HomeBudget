using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using Moq;
using HomeBudget.Service.Facade;
using System.Data.Entity.Core;

namespace HomeBudget.Test
{
    public class PaymentFacadeTest
    {
        private ICyclePaymentServices cyclePaymentServ;
        private ICounterServices counterServ;
        private ICommonTransactionsServices commonTransactionSer;

        private BudgetEntities InitData()
        {
            var bill = new List<CyclePayment>
            {
                new CyclePayment()
                {
                    Id=1,
                    SettlementPeriodId=1,
                    Name = "pay1",
                    Amount = 499,
                },

                new CyclePayment()
                {
                    Id =2,
                    SettlementPeriodId=1,
                    Name = "saves",
                    Amount = 345,
                    DestinationAccountGroupId = 2,

                },

                new CyclePayment()
                {
                    Id =3,
                    SettlementPeriodId=1,
                    Name = "samochod",
                    Amount = 200,
                    DestinationAccountGroupId = 2,
                },
                new CyclePayment()
                {
                    Id =4,
                    SettlementPeriodId=2,
                    Name = "czynsz Maj",
                    Amount = 499,
                }


            };

            var settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                },
                new SettlementPeriod()
                {
                    Id = 2,
                    Date = DateTime.Now
                }
            };

            var acountGroup = new List<AccountGroup>
            {
                new AccountGroup()
                {
                    Name = "biezace"
                },

                new AccountGroup()
                {
                    Name = "OKO"
                }
            };

            var transactions = new List<Transaction>();
            //{
            //    new Transaction()
            //    {
            //        Id=1,
            //        Name = "zap",
            //        Amount = 10,
            //        AccountGroupId = 1,
            //        SettlementPeriodId =1
            //    },
            //    new Transaction()
            //    {
            //        Id=2,
            //        Name = "zap",
            //        Amount = 100,
            //        AccountGroupId = 1,
            //        SettlementPeriodId =1
            //    }
            //};

            var counter = new List<Counter>
            {
                new Counter()
                {
                    Id = 1,
                    SettlementPeriodId =1,
                    Name = "electricity",
                    AmountCounter = 9856.6m,
                    Rate = 2.80m,
                    Equalized = false,
                    ForecastIncrease = null,
                    Equalizable=false
                },
                new Counter()
                {
                    Id = 2,
                    SettlementPeriodId =1,
                    Name = "water",
                    AmountCounter = 418.6m,
                    Rate = 5m,
                    Equalized = true,
                    DestinationAccountGroupId = 2,
                    ForecastIncrease = 7,
                    Equalizable=true

                },
                 new Counter()
                {
                    Id = 3,
                    SettlementPeriodId =1,
                    Name = "gas",
                    AmountCounter = 100.9m,
                    Rate = 5.5m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable = true

                },
                  new Counter()
                {
                    Id = 4,
                    SettlementPeriodId =2,
                    Name = "electricity",
                    AmountCounter = 9956.7m,
                    Rate = 2.80m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable = false

                },
                new Counter()
                {
                    Id = 5,
                    SettlementPeriodId =2,
                    Name = "water",
                    AmountCounter = 426m,
                    Rate = 5m,
                    Equalized = false,
                    ForecastIncrease = 7,
                    DestinationAccountGroupId = 2,
                    Equalizable = true
                },
                 new Counter()
                {
                    Id = 6,
                    SettlementPeriodId =2,
                    Name = "gas",
                    AmountCounter = 200.9m,
                    Rate = 5.5m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable = true

                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);


            db.SettlementPeriods.AddRange(settlementPeriods);
            db.CyclePayments.AddRange(bill);
            db.AccountGroups.AddRange(acountGroup);
            db.Transactions.AddRange(transactions);
            db.Counters.AddRange(counter);
            db.SaveChanges();

            return db;
        }

        #region PayCyclePaymentsTest
        [Fact]
        public void PayCyclePaymentsCheckTransactionOutgoesId()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(1, 1, 1);

            Xunit.Assert.Equal(1, db.CyclePayments.Find(1).TransactionOutgoesId);
        }

        [Fact]
        public void PayCyclePaymentsCheckNameOfTransaction()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(1, 1, 1);

            Xunit.Assert.Equal("pay1", db.Transactions.Find(1).Name);
        }

        [Fact]
        public void PayCyclePaymentsCheckAmount()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(1, 1, 1);

            Xunit.Assert.Equal(-499, db.Transactions.Find(1).Amount);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionOutgoesId()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal(1, db.CyclePayments.Find(2).TransactionOutgoesId);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionIncomesId()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal(2, db.CyclePayments.Find(2).TransactionIncomeId);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionOutgoesAmount()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal(-345, db.Transactions.Find(1).Amount);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionOutgoesName()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal("saves", db.Transactions.Find(1).Name);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionIncomesName()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal("saves", db.Transactions.Find(2).Name);
        }

        [Fact]
        public void PayInternalPaymentsCheckTransactionIncomesAmount()
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCyclePayments(2, 1, 1);

            Xunit.Assert.Equal(345, db.Transactions.Find(2).Amount);
        }

        #endregion PayCyclePaymentsTest

        #region PayCounter
        [Theory]
        [InlineData(10, 2, 1, 1)]
        
        public void CanPayCounterCheckWithoutId(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            Xunit.Assert.Throws(typeof(ObjectNotFoundException), () =>
            {
                objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);
            });
        }

        [Theory]
        [InlineData(4, 2, 1, 1)]
        [InlineData(5, 2, 1, 1)]
        [InlineData(6, 2, 1, 1)]
        public void CanPayCounterCheckOutgoesId(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId,sourceAccountGroupId);

            Xunit.Assert.Equal(1, db.Counters.Find(counterId).TransactionOutgoesId);
        }


        [Theory]
        [InlineData(4, 2, 1, 1, 0)]
        [InlineData(5, 2, 1, 1, -2)]
        [InlineData(6, 2, 1, 1, -550)]
        public void CanPayCounterCheckOutgoesAmount(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId, decimal amount)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(amount, db.Transactions.Find(1).Amount);
        }



        [Theory]
        [InlineData(4, 2, 1, 1, "electricity")]
        [InlineData(5, 2, 1, 1, "water")]
        [InlineData(6, 2, 1, 1, "gas")]
        public void CanPayCounterCheckOutgoesName(int counterId, int settlementPeriodId, int sourceAccountGroupId, int settlementPeriodPreviousId, string nameOfTransaction)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(nameOfTransaction, db.Transactions.Find(1).Name);
        }

        [Theory]
        [InlineData(4, 2, 1, 1, null)]
        [InlineData(5, 2, 1, 1, 2)]
        [InlineData(6, 2, 1, 1, null)]
        public void CanPayCounterCheckTransactionIncomeId(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId, int? incomesId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(incomesId, db.Counters.Find(counterId).TransactionIncomeId);
        }

        [Theory]
        
        [InlineData(5, 2, 1, 1, "water")]
        
        public void CanPayCounterCheckTransactionIncomeName(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId, string nameOfTransaction)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(nameOfTransaction, db.Transactions.Find(2).Name);
        }

        [Theory]
        [InlineData(4, 2, 1, 1)]
        [InlineData(6, 2, 1, 1)]
        public void CanPayCounterCheckTransactionIncomeNull(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Null(db.Transactions.Find(2));
        }

        [Theory]
        
        [InlineData(5, 2, 1, 1, 2)]
        
        public void CanPayCounterCheckIncomeAmount(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId, decimal amount)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(amount, db.Transactions.Find(2).Amount);
        }

        [Theory]
        [InlineData(4, 2, 1, 1)]
        [InlineData(6, 2, 1, 1)]
        public void CanPayCounterCheckIncomeAmountNull(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Null(db.Transactions.Find(2));
        }

        [Theory]
        [InlineData(4, 2, 1, 1)]
        [InlineData(5, 2, 1, 1)]
        [InlineData(6, 2, 1, 1)]
        public void CanPayCounterCheckTransactionEqualized(int counterId, int settlementPeriodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            BudgetEntities db = InitData();

            cyclePaymentServ = new CyclePaymentServices(db);
            counterServ = new CounterServices(db);
            commonTransactionSer = new CommonTransactionsServices(db);

            PaymentFacade objPaymentFacade = new PaymentFacade(cyclePaymentServ, counterServ, commonTransactionSer);

            objPaymentFacade.PayCounter(counterId, settlementPeriodId, settlementPeriodPreviousId, sourceAccountGroupId);

            Xunit.Assert.Equal(true, db.Counters.Find(counterId).Equalized);
        }
        #endregion PayCounter


    }

}
