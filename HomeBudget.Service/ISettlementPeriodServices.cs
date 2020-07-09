using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;

namespace HomeBudget.Service
{
    public interface ISettlementPeriodServices
    {
        int AddNewSettlementPeriod(DateTime date);

        DateTime GetSettlementPeriodDateById(int id);

        List<SettlementPeriod> GetListOfSettlementPeriods();

        int GetSettlementPeriodIdbyDate(DateTime date);


    }
}
