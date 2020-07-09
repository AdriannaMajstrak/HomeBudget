using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using HomeBudget.Service.Exceptions;

namespace HomeBudget.Test
{
    
    public class CommonTransactionsServicesTest
    {
    
        [Theory]
        [InlineData("premia")]
        [InlineData("d")]
        [InlineData("")]
        public void CanAddIncome(string nazwa)
        {
            var db = InitData();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.AddIncome(nazwa, 3, 1, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(nazwa, res.Last().Name);
        }

        [Theory]
        [InlineData("premia")]
        [InlineData("d")]
        [InlineData("")]
        public void CanAddIncome2(string nazwa)
        {
            var db = InitData();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var id = icrs.AddIncome(nazwa, 3, 1, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(id, res.Last().Id);
        }

        [Theory]
        [InlineData(null)]
        public void CanNotAddIncomeNull(string nazwa)
        {
            var db = InitData();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);

            Action actionToDo = () => { icrs.AddIncome(nazwa, 3, 1, 1); };

            Xunit.Assert.Throws<HomeBudgetServiceException>(actionToDo);


            //try
            //{
            //    icrs.AddIncome(nazwa, 3, 1, 1);
            //    Xunit.Assert.True(false);
            //}
            //catch (HomeBudgetServiceException ex)
            //{
            //    Xunit.Assert.True(true);
            //}
        }

        [Theory]
        [InlineData("piwo")]
        public void CanAddOutgoes1(string nazwa)
        {
            List<Transaction> transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Name = "jedzenie",
                    Amount = 10,
                    AccountGroupId = 1,
                    Id = 1,
                    SettlementPeriodId = 1
                }
            };

            List<AccountGroup> accountGroup = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id = 1,
                    Name = "G1"
                }
            };

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            int id = icrs.AddOutgoes(nazwa, 3, 1, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(nazwa, res.Last().Name);

        }

        [Theory]
        [InlineData("piwo")]
        public void CanAddOutgoes2(string nazwa)
        {
            List<Transaction> transactions = new List<Transaction>()
            {
                new Transaction()
                {
                    Name = "jedzenie",
                    Amount = 10,
                    AccountGroupId = 1,
                    Id = 1,
                    SettlementPeriodId = 1
                }
            };

            List<AccountGroup> accountGroup = new List<AccountGroup>()
            {
                new AccountGroup()
                {
                    Id = 1,
                    Name = "G1"
                }
            };

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            int id = icrs.AddOutgoes(nazwa, 3, 1, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(id, res.Last().Id);

        }

        #region InternalTransferTest
        [Theory]
        [InlineData(3)]
        public void CanDoInternalTransferFirst(int amount)
        {
            List<Transaction> transactions = new List<Transaction>();

            List<AccountGroup> accountGroup = new List<AccountGroup>()
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

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var tabelaId = icrs.DoInternalTransfer("na oszcz", amount, 1, 2, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(-amount, res.First().Amount);
        }

        [Theory]
        [InlineData (3)]
        public void CanDoInternalTransferFirst2(int amount)
        {
            List<Transaction> transactions = new List<Transaction>();
           
            List<AccountGroup> accountGroup = new List<AccountGroup>()
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

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var tabelaId = icrs.DoInternalTransfer("na oszcz", amount, 1,2, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(tabelaId[0], res.First().Id);
        }


        [Theory]
        [InlineData(3)]
        public void CanDoInternalTransferLast(int amount)
        {
            List<Transaction> transactions = new List<Transaction>();

            List<AccountGroup> accountGroup = new List<AccountGroup>()
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

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.DoInternalTransfer("na oszcz", amount, 1, 2, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(amount, res.Last().Amount);
        }

        [Theory]
        [InlineData(3)]
        public void CanDoInternalTransferLast2(int amount)
        {
            List<Transaction> transactions = new List<Transaction>();

            List<AccountGroup> accountGroup = new List<AccountGroup>()
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

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var tabelaId = icrs.DoInternalTransfer("na oszcz", amount, 1, 2, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(tabelaId[1], res.Last().Id);
        }

        [Fact]
        public void CanDoInternalTransferCount()
        {
            List<Transaction> transactions = new List<Transaction>();

            List<AccountGroup> accountGroup = new List<AccountGroup>()
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

            List<SettlementPeriod> settlementPeriods = new List<SettlementPeriod>()
            {
                new SettlementPeriod()
                {
                    Id = 1,
                    Date = DateTime.Now
                }
            };


            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            BudgetEntities db = new BudgetEntities(connection);

            db.Transactions.AddRange(transactions);
            db.AccountGroups.AddRange(accountGroup);
            db.SettlementPeriods.AddRange(settlementPeriods);
            db.SaveChanges();


            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.DoInternalTransfer("na oszcz", 3, 1, 2, 1);

            var res = db.Transactions.ToList();

            Xunit.Assert.Equal(2, res.Count());
        }

#endregion InternalTransferTest

        [Theory]
        [InlineData(1,1,1)]
        [InlineData(2,1,3)]
        [InlineData(2,2,4)]

        public void CanGetCommonTransactionsAndCheckFirst(int idAG, int idSP, int wynik)
        {

            BudgetEntities db = InitData();

            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var transactionsList = icrs.GetCommonTransactions(idAG, idSP);

            Xunit.Assert.Equal(transactionsList.ElementAt(0).Id, wynik);
        }


        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 1, 3)]
        [InlineData(2, 2, 4)]
        public void CanGetCommonTransactionsAndCheckLast(int idAG, int idSP, int wynik)
        {

            BudgetEntities db = InitData();

            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            var transactionsList = icrs.GetCommonTransactions(idAG, idSP);

            Xunit.Assert.Equal(transactionsList.Last().Id, wynik);
        }

        [Theory]
        [InlineData(1)]
        public void CanDeleteEntry(int id)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.DeleteEntryById(id);
            Xunit.Assert.False(db.Transactions.Any(x => x.Id == id));
        }

        #region Test of method EditIncome
        [Theory]
        [InlineData(1, "inny", 1, 2,2 )]
        public void CanEditIncomeName(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditIncome(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().Name, name);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditIncomeAmount(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditIncome(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().Amount, amount);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditIncomeAccountGroupId(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditIncome(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().AccountGroupId, idAccountGroup);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditIncomeSettlementPeriodId(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditIncome(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().SettlementPeriodId, idSettlementPeriod);
        }

        #endregion Test of method EditIncome

        #region Test of method EditOutgoes
        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditOutgoeseName(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditOutgoes(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().Name, name);
            Xunit.Assert.Equal(db.Transactions.First().SettlementPeriodId, idSettlementPeriod);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditOutgoesAmount(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditOutgoes(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().Amount, -amount);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditOutgoesAccountGroupId(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditOutgoes(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().AccountGroupId, idAccountGroup);
        }

        [Theory]
        [InlineData(1, "inny", 1, 2, 2)]
        public void CanEditOutgoesSettlementPeriodId(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            var db = InitData();
            ICommonTransactionsServices icrs = new CommonTransactionsServices(db);
            icrs.EditOutgoes(id, name, amount, idAccountGroup, idSettlementPeriod);
            Xunit.Assert.Equal(db.Transactions.First().SettlementPeriodId, idSettlementPeriod);
        }

        #endregion Test of method EditOutgoes


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
                    Date = DateTime.Now
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
    }

}
