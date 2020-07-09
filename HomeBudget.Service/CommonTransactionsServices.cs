using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;
using HomeBudget.Service.Exceptions;
using HomeBudget.Service.Model;

namespace HomeBudget.Service
{
    public class CommonTransactionsServices : ICommonTransactionsServices
    {
        private BudgetEntities dbContext;

        public CommonTransactionsServices(BudgetEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddIncome(string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            Transaction toAdd = new Transaction()
            {
                Name = name,
                Amount = amount,
                AccountGroupId = idAccountGroup,
                SettlementPeriodId = idSettlementPeriod
            };

            try
            {
                dbContext.Transactions.Add(toAdd);

                dbContext.SaveChanges();

                var id = toAdd.Id;

                return id;
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
            
        }

        public int AddOutgoes(string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            if(amount>0)
            {
                amount = -(amount);
            }

            Transaction toAdd = new Transaction()
            {
                Name = name,
                Amount = amount,
                AccountGroupId = idAccountGroup,
                SettlementPeriodId = idSettlementPeriod
            };

            try
            {
                dbContext.Transactions.Add(toAdd);

                dbContext.SaveChanges();

                return toAdd.Id;
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public int[] DoInternalTransfer(string name, decimal amount, int idAccountGroupFrom, int idAccountGroupTo, int idSettlementPeriod)
        {
            int idOutgoes = AddOutgoes(name, amount, idAccountGroupFrom, idSettlementPeriod);
            int idIncome = AddIncome(name, amount, idAccountGroupTo, idSettlementPeriod);

            return new[] { idOutgoes, idIncome };
        }

        public void DeleteEntryById(int id)
        {
            try
            {
                var el = dbContext.Transactions.Find(id);
                dbContext.Transactions.Remove(el);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void EditIncome(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            try
            {
                var toChange = dbContext.Transactions.Find(id);
                toChange.Name = name;
                toChange.Amount = amount;
                toChange.AccountGroupId = idAccountGroup;
                toChange.SettlementPeriodId = idSettlementPeriod;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }

        }

        public void EditOutgoes(int id, string name, decimal amount, int idAccountGroup, int idSettlementPeriod)
        {
            try
            {
                var toChange = dbContext.Transactions.Find(id);
                toChange.Name = name;
                toChange.Amount = - amount;
                toChange.AccountGroupId = idAccountGroup;
                toChange.SettlementPeriodId = idSettlementPeriod;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public List<Transaction> GetCommonTransactions(int idAccountGroup, int idSettlementPeriod)
        {
            List<Transaction> commonTransactions = dbContext.Transactions
                .Where(x => x.SettlementPeriodId == idSettlementPeriod)
                .Where(y => y.AccountGroupId == idAccountGroup).ToList();
                

                return commonTransactions;
        }

        public List<Transaction> GetOutgoesCommonTransactions(int idAccountGroup, int idSettlementPeriod)
        {
            var commonTransactionsModel = new CommonTransactions(this.GetCommonTransactions(idAccountGroup, idSettlementPeriod));
            return  commonTransactionsModel.ListOfOutgoes.ToList();
        }
    }
}
