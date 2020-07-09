using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;


namespace HomeBudget.Service.Facade
{
    public class SettlementPeriodFacade : ISettlementPeriodFacade
    {
        private ISettlementPeriodServices settlementPeriodServ;
        private ICyclePaymentServices billServ;
        private ICounterServices counterServ;

        public SettlementPeriodFacade(ISettlementPeriodServices sps, ICyclePaymentServices bs, ICounterServices cs)
        {
            settlementPeriodServ = sps;
            billServ = bs;
            counterServ = cs;
        }

        public int CreateNewSettlementPeriod(DateTime date)
        {
            int settlementPeriodId;
            settlementPeriodId = settlementPeriodServ.AddNewSettlementPeriod(date);
            billServ.GenerateCyclePaymentByPeriod(settlementPeriodId);
            counterServ.GenerateCounterByPeriod(settlementPeriodId);

            return settlementPeriodId;
        }
    }
}
