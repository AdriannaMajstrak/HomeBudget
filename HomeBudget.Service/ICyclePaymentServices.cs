using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public interface ICyclePaymentServices
    {
        void GenerateCyclePaymentByPeriod(int IdSettlementPeriod);

        void ChangeCyclePaymentAmount(int id, decimal newAmount);

        void SaveTransactionOutgoesId(int CyclePaymentId, int TransactionId);

        void SaveTransactionIncomeId(int CyclePaymentId, int TransactionId);

        void ChangeAccountGrupId(int idCyclePayment, int? newIdAccountGroup);

        void NotPayCyclePayemnts(int cyclePaymentsId);


        List<CyclePayment> GetCyclePayments(int idSettlementPeriod);

        //List<Bill> GetBills(int idSettlementPeriodStart, int idSettlementPeriodEnd);
    }
}
