using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public interface ICommonTransactionsServices
    {
        int AddIncome(string name, decimal amount, int idAccountGroup, int idSettlementPeriod);

        int AddOutgoes(string name, decimal amount, int idAccountGroup, int idSettlementPeriod);

        int[] DoInternalTransfer(string name, decimal amount, int idAccountGroupFrom, int idAccountGroupTo, int idSettlementPeriod);

        List<Transaction> GetCommonTransactions(int idAccountGroup, int idSettlementPeriod);

        void DeleteEntryById(int id);

        List<Transaction> GetOutgoesCommonTransactions(int idAccountGroup, int idSettlementPeriod);



        void EditIncome(int id, string name, decimal amount, int idAccountGroup,
            int idSettlementPeriod);

        void EditOutgoes(int id, string name, decimal amount, int idAccountGroup,
            int idSettlementPeriod);


    }


}
