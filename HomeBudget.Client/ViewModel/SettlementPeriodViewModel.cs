using HomeBudget.Client.Model;
using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.ViewModel
{
   public class SettlementPeriodViewModel : IPageViewModel
    {
        public SettlementPeriodViewModel(BudgetEntities budgetEntities)
        {

        }

        public event EventHandler<DataToRefresh> RefreshData;

        public void Refresh()
        { }
    }
}
