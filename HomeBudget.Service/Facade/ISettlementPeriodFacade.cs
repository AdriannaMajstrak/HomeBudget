using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Facade
{
    public interface ISettlementPeriodFacade
    {
        int CreateNewSettlementPeriod(DateTime date);
    }
}
