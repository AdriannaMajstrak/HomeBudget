using HomeBudget.DataAccess;
using HomeBudget.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public class CheckPointServices : ICheckPointServices
    {
        private BudgetEntities dbContext;

        public CheckPointServices(BudgetEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public int GenerateCheckPoint(DateTime date, int idSettlementPeriod)
        {

            var newChP = new CheckPoint()
            {
                Date = date,
                SettlementPeriodId = idSettlementPeriod
            };

            dbContext.CheckPoints.Add(newChP);
            dbContext.SaveChanges();

            return newChP.Id;
        }

        public void GenerateCheckPointEntry(int checkPointId, int accountGroupId, decimal amount)
        {
            try
            {
                dbContext.CheckPointEntries.Add(new CheckPointEntry()
                {
                    CheckPointId = checkPointId,
                    AccountGroupId = accountGroupId,
                    Amount = amount
                });

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void ChangeCheckPointEntry(int id, int checkPointId, int accountGroupId, decimal amount)
        {
            try
            {
                var toChange = dbContext.CheckPointEntries.Find(id);
                toChange.Amount = amount;
                toChange.CheckPointId = checkPointId;
                toChange.AccountGroupId = accountGroupId;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public List<CheckPoint> GetListOfCheckPoint(int IdSettlementPeriod)
        {
            var ListOfCheckPoint = dbContext.CheckPoints.Where(x => x.SettlementPeriodId == IdSettlementPeriod).ToList();

            return ListOfCheckPoint;
        }

        public List<CheckPointEntry> GetListOfCheckPointEntry(int IdCheckPoint)
        {
            var ListOfChPEntry = dbContext.CheckPointEntries.Where(x => x.CheckPointId == IdCheckPoint).ToList();

            return ListOfChPEntry;
        }
    }
}
