using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using System.Collections;

namespace HomeBudget.Test
{
    public class CheckPointServicesTest
    {
        private BudgetEntities InitDataChechPoint()
        {
            var ListSetlemenPeriod = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id =1,
                    Date = DateTime.Now
                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);
            db.SettlementPeriods.AddRange(ListSetlemenPeriod);

            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataChechPointEntry()
        {
            var ListCheckPoint = new List<CheckPoint>()
            {
                new CheckPoint()
                {
                    Id =1,
                    Date = new DateTime(1312, 12, 13),
                    SettlementPeriodId=1
                },

                new CheckPoint()
                {
                    Id =2,
                    Date = new DateTime(2322, 12, 23),
                    SettlementPeriodId=1
                }
            };

            var ListAccount = new List<Account>()
            {
                new Account()
                {
                    Id=1,
                    Name = "Konto Adama",
                    GroupId =1
                },
                new Account()
                {
                    Id=2,
                    Name = "Konto Ady",
                    GroupId =1
                }
            };

            var ListGroupAccount = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id=1,
                    Name = "Bierzace"
                }
            };

            var ListSetlemenPeriod = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id =1,
                    Date = DateTime.Now
                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);
            db.CheckPoints.AddRange(ListCheckPoint);
            db.Accounts.AddRange(ListAccount);
            db.AccountGroups.AddRange(ListGroupAccount);
            db.SettlementPeriods.AddRange(ListSetlemenPeriod);


            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataCheckPointEntry2()
        {
            var ListCheckPoint = new List<CheckPoint>()
            {
                new CheckPoint()
                {
                    Id =1,
                    Date = new DateTime(1312, 12, 13),
                    SettlementPeriodId=1
                },

                new CheckPoint()
                {
                    Id =2,
                    Date = new DateTime(2322, 12, 23),
                    SettlementPeriodId=1
                }
            };

            var ListAccount = new List<Account>()
            {
                new Account()
                {
                    Id=1,
                    Name = "Konto Adama",
                    GroupId =1
                },
                new Account()
                {
                    Id=2,
                    Name = "Konto Ady",
                    GroupId =1
                }
            };

            var ListGroupAccount = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id=1,
                    Name = "Bierzace"
                }
            };

            var ListSetlemenPeriod = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id =1,
                    Date = DateTime.Now
                }
            };

            var ListChP = new List<CheckPointEntry>()
            {
                new CheckPointEntry()
                {
                    Id=1,
                    CheckPointId =1,
                    AccountGroupId=1,
                    Amount =2.20m
                },
                new CheckPointEntry()
                {
                    Id=2,
                    CheckPointId =1,
                    AccountGroupId=2,
                    Amount =3.30m
                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);
            db.CheckPoints.AddRange(ListCheckPoint);
            db.Accounts.AddRange(ListAccount);
            db.AccountGroups.AddRange(ListGroupAccount);
            db.SettlementPeriods.AddRange(ListSetlemenPeriod);
            db.CheckPointEntries.AddRange(ListChP);

            db.SaveChanges();

            return db;
        }

        [Theory]
        [InlineData(06, 01, 1987)]
        [InlineData(13, 01, 1990)]
        public void CanGenerateCheckPoint(int day, int month, int year)
        {
            DateTime date = new DateTime(year, month,day );
            BudgetEntities db = InitDataChechPoint();
            CheckPointServices chps = new CheckPointServices(db);
            chps.GenerateCheckPoint(date,1);


            Xunit.Assert.Equal(date, db.CheckPoints.First().Date);
        }

        #region CanGenerateCheckPointEntry

        [Theory]
        [InlineData(1, 2, 1987)]
        [InlineData(2, 1, 1990)]
        public void CanGenerateCheckPointEntryID(int checkPointId, int accountId, decimal amount)
        {
            BudgetEntities db = InitDataChechPointEntry();
            CheckPointServices chps = new CheckPointServices(db);
            chps.GenerateCheckPointEntry(checkPointId, accountId, amount);

            Xunit.Assert.Equal(checkPointId, db.CheckPointEntries.First().CheckPointId);
        }

        [Theory]
        [InlineData(1, 2, 1987)]
        [InlineData(2, 1, 1990)]
        public void CanGenerateCheckPointEntryAcId(int checkPointId, int accountId, decimal amount)
        {
            BudgetEntities db = InitDataChechPointEntry();
            CheckPointServices chps = new CheckPointServices(db);
            chps.GenerateCheckPointEntry(checkPointId, accountId, amount);

            Xunit.Assert.Equal(accountId, db.CheckPointEntries.First().AccountGroupId);
        }

        [Theory]
        [InlineData(1, 2, 1987)]
        [InlineData(2, 1, 1990)]
        public void CanGenerateCheckPointEntryAmount(int checkPointId, int accountId, decimal amount)
        {
            BudgetEntities db = InitDataChechPointEntry();
            CheckPointServices chps = new CheckPointServices(db);
            chps.GenerateCheckPointEntry(checkPointId, accountId, amount);

            Xunit.Assert.Equal(amount, db.CheckPointEntries.First().Amount);
        }

        #endregion CanGenerateCheckPointEntry

        #region CanChangeCheckPointEntry

        [Theory]
        [InlineData(1, 2, 2, 11)]
        public void CanChangeCheckPointEntryCheckPointId(int id, int checkPointId, int accountId, decimal amount)
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            chps.ChangeCheckPointEntry(id, checkPointId, accountId, amount);

            Xunit.Assert.Equal(db.CheckPointEntries.Find(id).CheckPointId, checkPointId);
        }

        [Theory]
        [InlineData(1, 2, 2, 11)]
        public void CanChangeCheckPointEntryCheckPointAccount(int id, int checkPointId, int accountId, decimal amount)
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            chps.ChangeCheckPointEntry(id, checkPointId, accountId, amount);

            Xunit.Assert.Equal(db.CheckPointEntries.Find(id).AccountGroupId, accountId);
        }

        [Theory]
        [InlineData(1, 2, 2, 11)]
        public void CanChangeCheckPointEntryCheckPointAmount(int id, int checkPointId, int accountId, decimal amount)
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            chps.ChangeCheckPointEntry(id, checkPointId, accountId, amount);

            Xunit.Assert.Equal(db.CheckPointEntries.Find(id).Amount, amount);
        }

        #endregion CanChangeCheckPointEntry

        [Fact]
        public void CanGetListOfCheckPointFirst()
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            var list = chps.GetListOfCheckPoint(1);

            Xunit.Assert.Equal(db.CheckPoints.First().Date, list.First().Date);
        }

        [Fact]
        public void CanGetListOfCheckPointCount()
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            var list = chps.GetListOfCheckPoint(1);

            Xunit.Assert.Equal(db.CheckPoints.ToList().Count, list.Count());
        }

        [Fact]
        public void CanGetListOfCheckPointFirstEntry()
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            var list = chps.GetListOfCheckPointEntry(1);

            Xunit.Assert.Equal(db.CheckPointEntries.First().Amount, list.First().Amount);
        }

        [Fact]
        public void CanGetListOfCheckPointEntryCount()
        {
            var db = InitDataCheckPointEntry2();
            CheckPointServices chps = new CheckPointServices(db);
            var list = chps.GetListOfCheckPointEntry(1);

            Xunit.Assert.Equal(db.CheckPointEntries.ToList().Count, list.Count());
        }

    }


}
