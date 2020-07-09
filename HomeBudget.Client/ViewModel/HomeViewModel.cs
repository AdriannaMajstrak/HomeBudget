using HomeBudget.Client.Model;
using HomeBudget.Client.Utilities;
using HomeBudget.DataAccess;
using HomeBudget.Service;
using HomeBudget.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeBudget.Client.ViewModel
{
    public class HomeViewModel : ViewModelBase, IPageViewModel
    {
        ICommonTransactions commonTransaction;
        ICommonTransactionsServices commonTransactionsServices;
        private decimal commonMoney;

        public event EventHandler<DataToRefresh> RefreshData;

        public HomeViewModel()
        {
            commonTransactionsServices = new CommonTransactionsServices(new BudgetEntities());
            commonTransaction = new CommonTransactions(commonTransactionsServices.GetCommonTransactions(1, Properties.Settings.Default.ActualSettlementPeriodId));//biezacy settlement period
            CommonMoney = commonTransaction.TransactionsDifferent;
        }

        public string Test { get; set; } = "test ok";

        public decimal CommonMoney
        {
            get
            {
                return commonMoney;
            }
            set
            {
                commonMoney = value;
                Notify();
            }
        }

        public ICommand SwitchPage
        {
            get
            {
                return new RelayCommand<string>((s) => Navigation?.Invoke(this, s));
            }
        }

        public event EventHandler<string> Navigation;

        public void Refresh()
        { }

       



    }
}
