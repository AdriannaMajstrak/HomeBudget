using HomeBudget.DataAccess;
using HomeBudget.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public class CounterServices : ICounterServices
    {
        private BudgetEntities dbContext;


        public CounterServices(BudgetEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateCounterByPeriod(int idSettlementPeriod)
        {
            try
            {
                foreach (var item in dbContext.CounterTemplates)
                {
                    var addedCounter = new Counter()
                    {
                        Name = item.Name,
                        Rate = item.Rate,
                        SettlementPeriodId = idSettlementPeriod,
                        AmountCounter = 0,
                        TransactionOutgoesId = null,
                        TransactionIncomeId =null,
                        DestinationAccountGroupId = item.DestinationAccountGroupId,
                        ForecastIncrease = item.ForecastIncrease
                    };

                    addedCounter.Equalized = !item.Equalizable;
                    addedCounter.Equalizable = item.Equalizable;


                    dbContext.Counters.Add(addedCounter);
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void ChangeCounterAmount(int id, decimal counterAmount)
        {
            try
            {
                var toChange = dbContext.Counters.Find(id);
                toChange.AmountCounter = counterAmount;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void PayEqualized(int idCounter, int idTransactionOutgoes)
        {
            try
            {
                var toChange = dbContext.Counters.Find(idCounter);
                
                toChange.Equalized = true;
                toChange.TransactionOutgoesId = idTransactionOutgoes;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void PayEqualized(int idCounter, int idTransactionOutgoes, int idTransactionIncome)
        {
            try
            {
                var toChange = dbContext.Counters.Find(idCounter);

                toChange.Equalized = true;
                toChange.TransactionOutgoesId = idTransactionOutgoes;
                toChange.TransactionIncomeId = idTransactionIncome;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void NotPayEqualized(int idCounter)
        {
            try
            {
                
                var toChange = dbContext.Counters.Find(idCounter);
              
                toChange.Equalized = false;

                var tr1 = toChange.TransactionOutgoesId;
                var tr2 = toChange.TransactionIncomeId;


                if(tr1 != null)
                {
                    dbContext.Transactions.Remove(dbContext.Transactions.Find((int)tr1));
                }

                if (tr2 != null)
                {
                    dbContext.Transactions.Remove(dbContext.Transactions.Find((int)tr2));
                }

                toChange.TransactionOutgoesId = null;
                toChange.TransactionIncomeId = null;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public List<Counter> GetCounters(int idSettlementPeriod)
        {
            List<Counter> ListOfCounter = dbContext.Counters
                .Where(x => x.SettlementPeriodId == idSettlementPeriod)
                .ToList();

            return ListOfCounter;
        }
    }
}
