using HomeBudget.DataAccess;
using HomeBudget.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Facade
{
    public interface IPaymentFacade
    {
        void PayCyclePayments(int billId, int settlementperiodId, int sourceAccountGroupId);

        void PayCounter(int counterId, int settlementperiodId, int settlementPeriodPreviousId, int sourceAccountGroupId);

        void SaveCounterAmount(int id, decimal counterAmount);

        ICalculatedCounter CalculatePaymentForCounter(int counterId, int settlementperiodId, int settlementPeriodPreviousId);

        ICalculatedCounter CalculatePaymentForCounter(int counterId, List<Counter> listOfActualCounters, int settlementPeriodPreviousId);

        void NotPayCounter(int idCounter);
    }
}
