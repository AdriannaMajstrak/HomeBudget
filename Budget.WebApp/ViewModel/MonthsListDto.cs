using HomeBudget.DataAccess;
using System.Collections.Generic;

namespace Budget.WebApp.ViewModel
{
    public class MonthsListDto
    {
        public IList<MonthsDto> Months;

      //  public IList<Transaction> listOfCommonOutgoesTransactions = new List<Transaction>();
    }
}