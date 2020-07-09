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
    public class SettlementPeriodServicesTest
    {
        private BudgetEntities InitData()
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
            var accountGroup = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id = 1,
                    Name = "G1"
                },
                new AccountGroup()
                {
                    Id = 2,
                    Name = "G2"
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
                    Date = new DateTime(2018, 2, 13)
                }
            };
            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();



            return db;
        }

        [Fact]
        public void CanAddNewSettlementPeriod()
        {
            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);

            //var db = InitData();

            var date = (new DateTime(1990, 1, 13));

            ISettlementPeriodServices spServices = new SettlementPeriodServices(db);

            spServices.AddNewSettlementPeriod(date);

            var d = db.SettlementPeriods.FirstOrDefault(x => x.Id == db.SettlementPeriods.Max(i => i.Id));

            Xunit.Assert.Equal(date, d.Date);
        }

        [Fact]
        public void CanGetListOfSettlementPeriodsCount()
        {
            var db = InitData();
            var sps = new SettlementPeriodServices(db);
            var list = sps.GetListOfSettlementPeriods();

            Xunit.Assert.Equal(db.SettlementPeriods.ToList().Count, list.Count());
        }

        [Fact]
        public void CanGetListOfSettlementPeriodsFirst()
        {
            var db = InitData();
            var sps = new SettlementPeriodServices(db);
            var list = sps.GetListOfSettlementPeriods();

            Xunit.Assert.Equal(db.SettlementPeriods.First().Date, list.First().Date);
        }

        [Fact]
        public void CanGetSettlementPeriodIdbyDate()
        {
            var db = InitData();
            var sps = new SettlementPeriodServices(db);
            var id = sps.GetSettlementPeriodIdbyDate(DateTime.Now);

            Xunit.Assert.Equal(1, id);

        }
    }
}
