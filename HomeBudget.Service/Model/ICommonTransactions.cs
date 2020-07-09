using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;
using System.Runtime.CompilerServices;

namespace HomeBudget.Service.Model
{
    public interface ICommonTransactions
    {
        IEnumerable<Transaction> ListOfAllTransactions { get; set; }
        IEnumerable<Transaction> ListOfIncome { get; set; }
        IEnumerable<Transaction> ListOfOutgoes { get; set; }
        decimal SumOfIncome { get; set; }
        decimal SumOfOutgoes { get; set; }
        decimal TransactionsDifferent { get; set; }
        
    }
}
