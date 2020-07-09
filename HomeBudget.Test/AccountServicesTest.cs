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
    public class AccountServicesTest
    {
        private BudgetEntities InitData()
        {
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

            List<Account> ListOfAccounts = new List<Account>
            {
                new Account()
                {
                    Name = "ING Adam",
                    GroupId = 1
                },
                new Account()
                {
                    Name = "ING Ada",
                    GroupId =1
                },
                new Account()
                {
                    Name = "OKO Adam",
                    GroupId = 2
                },
                new Account()
                {
                    Name = "OKO Ada",
                    GroupId =2
                }

            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);


            db.AccountGroups.AddRange(ListAccountGroup);
            db.Accounts.AddRange(ListOfAccounts);
            db.SaveChanges();

            return db;
        }

        [Fact]
        public void CanGetAllAccountGroupsCount()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccountGroup = ias.GetAllAccountGroups();

            Xunit.Assert.Equal(db.AccountGroups.ToList().Count, listAccountGroup.Count());
        }

        [Fact]
        public void CanGetAllAccountGroupsFirst()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccountGroup = ias.GetAllAccountGroups();

            Xunit.Assert.Equal(db.AccountGroups.First().Name, listAccountGroup.First().Name);
        }

        [Fact]
        public void CanGetAllAccountGroupsLast()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccountGroup = ias.GetAllAccountGroups();

            Xunit.Assert.Equal(db.AccountGroups.ToList().Last().Name, listAccountGroup.Last().Name);
        }


        [Fact]
        public void CanGetAllAccountsCount()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccount = ias.GetAllAccounts();

            Xunit.Assert.Equal(db.Accounts.ToList().Count, listAccount.Count());
        }

        [Fact]
        public void CanGetAllAccountsFirst()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccount = ias.GetAllAccounts();

            Xunit.Assert.Equal(db.Accounts.First().Name, listAccount.First().Name);
        }

        [Fact]
        public void CanGetAllAccountsLast()
        {
            var db = InitData();
            IAccountServices ias = new AccountServices(db);
            var listAccount = ias.GetAllAccounts();

            Xunit.Assert.Equal(db.Accounts.ToList().Last().Name, listAccount.Last().Name);
        }

    }
}
