using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public interface ICounterServices
    {
        void GenerateCounterByPeriod(int IdSettlementPeriod);

        void ChangeCounterAmount(int id, decimal counterAmount);

        void PayEqualized(int idCounter, int idTransaction);

        void PayEqualized(int idCounter, int idTransactionOutgoes, int idTransactionIncome);

        void NotPayEqualized(int idCounter);



        List<Counter> GetCounters(int idSettlementPeriod);

        //List<Counter> GetCounters(int idSettlementPeriodStart, int idSettlementPeriodEnd);

    }
}
