using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;

using System.Linq;
using Xunit;
using System.Collections;
using HomeBudget.Service;
using HomeBudget.Service.Facade;

namespace HomeBudget.Test
{
    public class CheckPointFasadeTest
    {
        private BudgetEntities InitData()
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
                },
                new Account()
                {
                    Id=3,
                    Name = "OKO Adama",
                    GroupId =2
                },
                new Account()
                {
                    Id=4,
                    Name = "OKO Ady",
                    GroupId =2
                }

            };

            var ListGroupAccount = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id=1,
                    Name = "Bierzace"
                },
                new AccountGroup()
                {
                    Id=2,
                    Name = "oszcz"
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
                },
                new CheckPointEntry()
                {
                    Id=3,
                    CheckPointId =1,
                    AccountGroupId=3,
                    Amount =300m
                },
                new CheckPointEntry()
                {
                    Id=4,
                    CheckPointId =1,
                    AccountGroupId=4,
                    Amount =400m
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


        //[Theory]
        //[InlineData(100,200,-100)]
        //[InlineData(200,100,100)]
        //[InlineData(100,100,0)]
        //public void CanGetDifferentActualAndExpectedAmount(decimal realAmount, decimal expectedAmount, decimal result)
        //{
        //    var ChPFasade = new CheckPointFacade(InitData());
        //    var  res = ChPFasade.GetDifferentActualAndExpectedAmount(realAmount, expectedAmount);
        //    Xunit.Assert.Equal(result, res);
        //}

        [Theory]
       [InlineData(1, 1, 2.2)]
        [InlineData(1, 4, 400)]
        public void CanSumMoneyOnAccountGroup(int idCheckPoint, int idAccountGroup, decimal exResult)
        {
            var ChPFasade = new CheckPointFacade(new CheckPointServices(InitData()));
            var actualRes = ChPFasade.SumMoneyOnAccountGroup(idCheckPoint, idAccountGroup);
            Xunit.Assert.Equal(exResult, actualRes);

        }
    }
}
