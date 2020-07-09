using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;

namespace HomeBudget.Test
{
    public class CounterServicesTest
    {
        private BudgetEntities InitDataCounterTemplates()
        {
            var counterTemplates = new List<CounterTemplate>()
            {
                new CounterTemplate()
                {
                    Name = "prąd",
                    Rate = 2.80m,
                    Equalizable = false
                },
                new CounterTemplate()
                {
                    Name = "woda",
                    Rate = 5m,
                    Equalizable = true
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
            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);

           
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.CounterTemplates.AddRange(counterTemplates);
            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataCounterList()
        {
            var counter = new List<Counter>()
            {
                new Counter()
                {
                    Id = 1,
                    SettlementPeriodId =1,
                    Name = "electricity",
                    AmountCounter = 9854.4m,
                    Rate = 2.80m,
                    Equalized = true
                },
                new Counter()
                {
                    Id = 2,
                    SettlementPeriodId =1,
                    Name = "water",
                    AmountCounter = 418.6m,
                    Rate = 5m,
                    Equalized = false
                },
                new Counter()
                {
                    Id = 3,
                    SettlementPeriodId =2,
                    Name = "electricity",
                    AmountCounter = 9994.7m,
                    Rate = 2.80m,
                    Equalized = true
                },
                new Counter()
                {
                    Id = 4,
                    SettlementPeriodId =2,
                    Name = "water",
                    AmountCounter = 500.8m,
                    Rate = 5m,
                    Equalized = false
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

            var transactions = new List<Transaction>
            {
                new Transaction()
                {
                    Id=1,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=2,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=3,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=4,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=5,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=6,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },

            };

            List<AccountGroup> ListAccountGroup = new List<AccountGroup>
            {
                new AccountGroup()
                {
                    Id=1,
                    Name = "Konta bieżące"
                },
                new AccountGroup()
                {
                    Id =2,
                    Name = "OKO"
                }
            };



            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);

            db.SettlementPeriods.AddRange(settlementPeriods);
            db.Counters.AddRange(counter);
            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(ListAccountGroup);

            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataCounterNotPayEqualize()
        {
            var counter = new List<Counter>()
            {
                new Counter()
                {
                    Id = 1,
                    SettlementPeriodId =1,
                    Name = "electricity",
                    AmountCounter = 9854.4m,
                    Rate = 2.80m,
                    Equalized = true,
                    TransactionOutgoesId=1
                },
                new Counter()
                {
                    Id = 2,
                    SettlementPeriodId =1,
                    Name = "water",
                    AmountCounter = 418.6m,
                    Rate = 5m,
                    Equalized = true,
                    TransactionOutgoesId=3,
                    TransactionIncomeId =4
                },
                new Counter()
                {
                    Id = 3,
                    SettlementPeriodId =2,
                    Name = "electricity",
                    AmountCounter = 9994.7m,
                    Rate = 2.80m,
                    Equalized = false
                },
                new Counter()
                {
                    Id = 4,
                    SettlementPeriodId =2,
                    Name = "water",
                    AmountCounter = 500.8m,
                    Rate = 5m,
                    Equalized = false
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

            var transactions = new List<Transaction>
            {
                new Transaction()
                {
                    Id=1,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=2,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=3,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=4,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=5,
                    Name = "zap",
                    Amount = 10,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=6,
                    Name = "zap",
                    Amount = 100,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },

            };

            List<AccountGroup> ListAccountGroup = new List<AccountGroup>
            {
                new AccountGroup()
                {
                    Id=1,
                    Name = "Konta bieżące"
                },
                new AccountGroup()
                {
                    Id =2,
                    Name = "OKO"
                }
            };



            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);

            db.SettlementPeriods.AddRange(settlementPeriods);
            db.Counters.AddRange(counter);
            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(ListAccountGroup);

            db.SaveChanges();

            return db;
        }



        #region test GenerateCounter
        [Fact]
        public void CanGenerateCounterByPeriodFirstName()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);

            Xunit.Assert.Equal(db.CounterTemplates.First().Name, db.Counters.First().Name);
        }

        [Fact]
        public void CanGenerateCounterByPeriodLastName()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);
            var testListActual = db.Counters.ToList();
            var testListEx = db.CounterTemplates.ToList();

            Xunit.Assert.Equal(testListEx.Last().Name, testListActual.Last().Name);

        }

        [Fact]
        public void CanGenerateCounterByPeriodFirstRate()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);

            Xunit.Assert.Equal(db.CounterTemplates.First().Rate, db.Counters.First().Rate);
        }

        [Fact]
        public void CanGenerateCounterByPeriodLastRate()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);

            var testListActual = db.Counters.ToList();
            var testListEx = db.CounterTemplates.ToList();

            Xunit.Assert.Equal(testListEx.Last().Rate, testListActual.Last().Rate);
        }

        [Fact]
        public void CanGenerateCounterByPeriodEqalizable()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);

            Xunit.Assert.NotEqual(db.CounterTemplates.First().Equalizable, db.Counters.First().Equalized);
        }

        [Fact]
        public void CanGenerateCounterByPeriodLasEqualizable()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);
            var testListActual = db.Counters.ToList();
            var testListEx = db.CounterTemplates.ToList();

            Xunit.Assert.NotEqual(testListEx.Last().Equalizable, testListActual.Last().Equalized);
        }

        [Fact]
        public void CanGenerateCounterByPeriodAmount()
        {
            var db = InitDataCounterTemplates();
            ICounterServices ics = new CounterServices(db);
            ics.GenerateCounterByPeriod(1);
            var testAmount = db.Counters.ToList();
            Xunit.Assert.Equal(0, testAmount.Last().AmountCounter );
        }

        #endregion test GenerateCounter

        [Theory]
        [InlineData (1, 10) ]
        public void CanChangeCounterAmount(int id, decimal counterAmount)
        {
            var db = InitDataCounterList();

            CounterServices cs = new CounterServices(db);
            cs.ChangeCounterAmount(id, counterAmount);

            var act = db.Counters.ToList();

            Xunit.Assert.Equal(counterAmount, act.First().AmountCounter);
        }

        

        [Theory]
        [InlineData(1, "electricity")]
        public void CanGetCounters1(int settlementId, string name)
        {
            var db = InitDataCounterList();

            var objCounterServices = new CounterServices(db);
            var counterList= objCounterServices.GetCounters(settlementId).ToList();


            Xunit.Assert.Equal(name, counterList.First().Name);
        }

        #region CanPayEqualized

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        public void CanPayEqualized(int counter,int idTranOutgoes)
        {
            var db = InitDataCounterList();
            var objCounterServices = new CounterServices(db);
            objCounterServices.PayEqualized(counter, idTranOutgoes);

            Xunit.Assert.Equal(idTranOutgoes, db.Counters.Find(counter).TransactionOutgoesId);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(2, 4, 5)]
        public void CanPayEqualizedOutgoesId(int counter, int idTranOutgoes, int idTranIncomes)
        {
            var db = InitDataCounterList();
            var objCounterServices = new CounterServices(db);
            objCounterServices.PayEqualized(counter, idTranOutgoes, idTranIncomes);

            Xunit.Assert.Equal(idTranOutgoes, db.Counters.Find(counter).TransactionOutgoesId);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 4)]
        public void CanPayEqualizedIncomesId(int counter, int idTranOutgoes, int idTranIncomes)
        {
            var db = InitDataCounterList();
            var objCounterServices = new CounterServices(db);
            objCounterServices.PayEqualized(counter, idTranOutgoes, idTranIncomes);

            Xunit.Assert.Equal(idTranIncomes, db.Counters.Find(counter).TransactionIncomeId);
        }
        #endregion CanPayEqualized

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayEqualized(int idCounter)
        {
            var db = InitDataCounterNotPayEqualize();

            var objConterServices = new CounterServices(db);

            int? transactionId = db.Counters.Find(idCounter).TransactionOutgoesId;
            objConterServices.NotPayEqualized(idCounter);

            Xunit.Assert.Null(db.Transactions.Find(transactionId));
        }

        [Theory]
        [InlineData(2)]
        public void CanNotPayEqualizedSecondTransaction(int idCounter)
        {
            var db = InitDataCounterNotPayEqualize();

            var objConterServices = new CounterServices(db);

            int? transactionId = db.Counters.Find(idCounter).TransactionIncomeId;
            objConterServices.NotPayEqualized(idCounter);

            Xunit.Assert.Null(db.Transactions.Find(transactionId));
        }

       

        [Theory]
        [InlineData(1)]
        public void CanNotPayEqualizedAnotherTransaction(int idCounter)
        {
            var db = InitDataCounterNotPayEqualize();

            var objConterServices = new CounterServices(db);

            int? transactionId = db.Counters.Find(idCounter).TransactionIncomeId;
            objConterServices.NotPayEqualized(idCounter);

            Xunit.Assert.NotNull(db.Transactions.Find(2));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayEqualizedTransactionOutgoesIdinCounter(int idCounter)
        {
            var db = InitDataCounterNotPayEqualize();

            var objConterServices = new CounterServices(db);

            objConterServices.NotPayEqualized(idCounter);

            Xunit.Assert.Null(db.Counters.Find(idCounter).TransactionOutgoesId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayEqualizedTransactionIncomeIdinCounter(int idCounter)
        {
            var db = InitDataCounterNotPayEqualize();

            var objConterServices = new CounterServices(db);

            objConterServices.NotPayEqualized(idCounter);

            Xunit.Assert.Null(db.Counters.Find(idCounter).TransactionIncomeId);
        }
    }
}
