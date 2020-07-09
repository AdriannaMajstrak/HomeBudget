using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public class SettlementPeriodServices : ISettlementPeriodServices
    {
        private BudgetEntities dbContext;

        public SettlementPeriodServices(BudgetEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddNewSettlementPeriod(DateTime date)
        {
            SettlementPeriod objSettPeriod = new SettlementPeriod()
            {
                Date = date
            };
            dbContext.SettlementPeriods.Add(objSettPeriod);

            dbContext.SaveChanges();
            return objSettPeriod.Id;
        }

        public List<SettlementPeriod> GetListOfSettlementPeriods()
        {
            var ListOfSettlementPeriods = dbContext.SettlementPeriods.ToList();

            return ListOfSettlementPeriods;
        }

        public DateTime GetSettlementPeriodDateById(int id)
        {
            return dbContext.SettlementPeriods.Find(id).Date;
        }

        public int GetSettlementPeriodIdbyDate(DateTime date)
        {
            return dbContext.SettlementPeriods.Single(x => x.Date.Month == date.Month && x.Date.Year == date.Year).Id; ////czemu sierpien 2017?
        }

    }
}
