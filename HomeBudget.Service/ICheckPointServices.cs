using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public interface ICheckPointServices
    {
        int GenerateCheckPoint(DateTime date, int idSettlementPeriod);

        void GenerateCheckPointEntry(int checkPointId, int accountId, decimal amount);

        void ChangeCheckPointEntry(int id, int checkPointId, int accountId, decimal amount);

        List<CheckPoint> GetListOfCheckPoint(int IdSettlementPeriod);

        List<CheckPointEntry> GetListOfCheckPointEntry(int IdCheckPoint);
    }
}
