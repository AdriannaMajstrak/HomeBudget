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
    public class CyclePaymentTest
    {
        private BudgetEntities InitDataBillTemplates()
        {
            var billTemplates = new List<CyclePaymentTemplate>
            {
                new CyclePaymentTemplate()
                {
                    Name = "czynsz",
                    Amount = 499,
                },

                new CyclePaymentTemplate()
                {
                    Name = "studia",
                    Amount = 345,
                    AccountGroupId = 1
                },

                new CyclePaymentTemplate()
                {
                    Name = "samochod",
                    Amount = 200,
                    AccountGroupId = 2
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
                    Name = "Na studia"
                },

                new AccountGroup()
                {
                    Name = "Na samochod"
                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);


            db.SettlementPeriods.AddRange(settlementPeriods);
           db.CyclePaymentTemplates.AddRange(billTemplates);
            db.AccountGroups.AddRange(acountGroup);
            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataBill()
        {
            var bill = new List<CyclePayment>
            {
                new CyclePayment()
                {
                    Id=1,
                    SettlementPeriodId=1,
                    Name = "czynsz",
                    Amount = 499,
                },

                new CyclePayment()
                {
                    Id =2,
                    SettlementPeriodId=1,
                    Name = "studia",
                    Amount = 345,
                    DestinationAccountGroupId = 1,

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
                    Name = "Na studia"
                },

                new AccountGroup()
                {
                    Name = "Na samochod"
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
                }
            };




            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);


            db.SettlementPeriods.AddRange(settlementPeriods);
            db.CyclePayments.AddRange(bill);
            db.AccountGroups.AddRange(acountGroup);
            db.Transactions.AddRange(transactions);
            db.SaveChanges();

            return db;
        }

        private BudgetEntities InitDataBillNotPay()
        {
            var bill = new List<CyclePayment>
            {
                new CyclePayment()
                {
                    Id=1,
                    SettlementPeriodId=1,
                    Name = "czynsz",
                    Amount = 499,
                    TransactionOutgoesId =1
                },

                new CyclePayment()
                {
                    Id =2,
                    SettlementPeriodId=1,
                    Name = "studia",
                    Amount = 345,
                    DestinationAccountGroupId = 1,
                    TransactionOutgoesId =2,
                    TransactionIncomeId =3,

                },

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
                    Name = "Na studia"
                },

                new AccountGroup()
                {
                    Name = "Na samochod"
                }
            };

            var transactions = new List<Transaction>
            {
                new Transaction()
                {
                    Id=1,
                    Name = "zap",
                    Amount = 499,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=2,
                    Name = "zap",
                    Amount = 395,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                },
                new Transaction()
                {
                    Id=3,
                    Name = "zap",
                    Amount = 395,
                    AccountGroupId = 1,
                    SettlementPeriodId =1
                }
            };

            var connection = EntityConnectionFactory.CreateTransient("name=BudgetEntities");
            var db = new BudgetEntities(connection);


            db.SettlementPeriods.AddRange(settlementPeriods);
            db.CyclePayments.AddRange(bill);
            db.AccountGroups.AddRange(acountGroup);
            db.Transactions.AddRange(transactions);
            db.SaveChanges();

            return db;
        }

        #region Test GenerateBillByPeriod
        [Fact]
        public void CanGenerateBillByPeriodFirstName()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);
            
            Xunit.Assert.Equal(db.CyclePaymentTemplates.First().Name, db.CyclePayments.First().Name);
        }

        [Fact]
        public void CanGenerateBillByPeriodLAsttName()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);
            var billTemp = db.CyclePaymentTemplates.ToList();
            var bill = db.CyclePayments.ToList();

            Xunit.Assert.Equal(billTemp.Last().Name, bill.Last().Name );
        }

        [Fact]
        public void CanGenerateBillByPeriodFirstAcoundGroup()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);

            Xunit.Assert.Equal(db.CyclePaymentTemplates.First().AccountGroupId, db.CyclePayments.First().DestinationAccountGroupId);
        }

        [Fact]
        public void CanGenerateBillByPeriodLAsttAcoundGroup()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);
            var billTemp = db.CyclePaymentTemplates.ToList();
            var bill = db.CyclePayments.ToList();

            Xunit.Assert.Equal(billTemp.Last().AccountGroupId, bill.Last().DestinationAccountGroupId);
        }

        [Fact]
        public void CanGenerateBillByPeriodFirstPaymentId()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);

            Xunit.Assert.Null(db.CyclePayments.First().TransactionOutgoesId);
        }

        [Fact]
        public void CanGenerateBillByPeriodLastPaymentId()
        {
            var db = InitDataBillTemplates();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.GenerateCyclePaymentByPeriod(1);
            var bill = db.CyclePayments.ToList();

            Xunit.Assert.Null(bill.Last().TransactionOutgoesId);
        }
        #endregion Test GenerateBillByPeriod

        [Theory]
        [InlineData(1,3)]
        [InlineData(2,1)]
        public void CanGetBillsCountOfList(int idSettlementPeriod, int countOfList)
        {
            var db = InitDataBill();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            var listOfBills = bs.GetCyclePayments(idSettlementPeriod);
            Xunit.Assert.Equal(countOfList, listOfBills.Count());
        }

        [Fact]
        public void CanGetBillsCountLastName()
        {
            var db = InitDataBill();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            var listOfBills = bs.GetCyclePayments(1);
            Xunit.Assert.Equal("samochod", listOfBills.ToList().Last().Name);
        }


        [Theory]
        [InlineData(1, 99)]
        [InlineData(2, 98)]
        public void CanChangeBillAmount(int idBill, decimal newAmount)
        {
            var db = InitDataBill();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.ChangeCyclePaymentAmount(idBill, newAmount);

            Xunit.Assert.Equal(newAmount, db.CyclePayments.Where(x => x.Id == idBill).ToList().First().Amount);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(2, null)]
        public void CanChangeAccountGroupId(int idBill, int? newIdAccountGroup)
        {
            var db = InitDataBill();
            ICyclePaymentServices bs = new CyclePaymentServices(db);
            bs.ChangeAccountGrupId(idBill, newIdAccountGroup);

            Xunit.Assert.Equal(newIdAccountGroup, db.CyclePayments.Where(x => x.Id == idBill).ToList().First().DestinationAccountGroupId);
        }

        [Theory]
        [InlineData(1,2)]
        [InlineData(2,1)]
        public void CanSaveTransactionOutgoesId(int idBill, int IdPayment)
        {
            var db = InitDataBill();
            var bs = new CyclePaymentServices(db);
            bs.SaveTransactionOutgoesId(idBill, IdPayment);

            Xunit.Assert.Equal(IdPayment, db.CyclePayments.Find(idBill).TransactionOutgoesId);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void CanSaveTransactionIncomeId(int idBill, int IdPayment)
        {
            var db = InitDataBill();
            var bs = new CyclePaymentServices(db);
            bs.SaveTransactionIncomeId(idBill, IdPayment);

            Xunit.Assert.Equal(IdPayment, db.CyclePayments.Find(idBill).TransactionIncomeId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayCyclePayemntsOutgoesId(int cyclePaymentsId)
        {
            var db = InitDataBillNotPay();
            CyclePaymentServices cyclePayment = new CyclePaymentServices(db);

            cyclePayment.NotPayCyclePayemnts(cyclePaymentsId);
            Xunit.Assert.Equal(null, db.CyclePayments.Find(cyclePaymentsId).TransactionOutgoesId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayCyclePayemntsOutgoesTransaction(int cyclePaymentsId)
        {
            var db = InitDataBillNotPay();
            CyclePaymentServices cyclePayment = new CyclePaymentServices(db);

            var transactionOugoesId = db.CyclePayments.Find(cyclePaymentsId).TransactionOutgoesId;
            cyclePayment.NotPayCyclePayemnts(cyclePaymentsId);
            Xunit.Assert.Null(db.Transactions.Find(transactionOugoesId));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayCyclePayemntsIncomeId(int cyclePaymentsId)
        {
            var db = InitDataBillNotPay();
            CyclePaymentServices cyclePayment = new CyclePaymentServices(db);

            cyclePayment.NotPayCyclePayemnts(cyclePaymentsId);
            Xunit.Assert.Equal(null, db.CyclePayments.Find(cyclePaymentsId).TransactionIncomeId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanNotPayCyclePayemntsIncomeTransaction(int cyclePaymentsId)
        {
            var db = InitDataBillNotPay();
            CyclePaymentServices cyclePayment = new CyclePaymentServices(db);

            var transactionIncomeId = db.CyclePayments.Find(cyclePaymentsId).TransactionIncomeId;
            cyclePayment.NotPayCyclePayemnts(cyclePaymentsId);
            Xunit.Assert.Null(db.Transactions.Find(transactionIncomeId));
        }
    }


}
