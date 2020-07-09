using HomeBudget.Client.Model;
using HomeBudget.Client.Utilities;
using HomeBudget.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeBudget.Client.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private HashSet<DataToRefresh> dataToRefreshList = new HashSet<DataToRefresh>();

        public MainWindowViewModel(BudgetEntities budgetEntities)
        {
            homeViewModel = new HomeViewModel();
            outgoesViewModel = new OutgoesViewModel(budgetEntities);
            incomeViewModel = new IncomeViewModel(budgetEntities);
            savingsViewModel = new SavingsViewModel(budgetEntities);
            checkPointViewModel = new CheckPointViewModel(budgetEntities);
            counterViewModel = new CounterViewModel(budgetEntities);
            cyclePaymentsViewModel = new CyclePaymentsViewModel(budgetEntities);
            settlementPeriodViewModel = new SettlementPeriodViewModel(budgetEntities);

            CurrentPage = homeViewModel;
            PageSwitcher = new RelayCommand<string>(ChangePage);
            ((HomeViewModel)homeViewModel).Navigation += (sender, page) => { ChangePage(page); };

            counterViewModel.RefreshData += AddDataToRefresh;
            cyclePaymentsViewModel.RefreshData += AddDataToRefresh;
        }

        private void AddDataToRefresh(object sender, DataToRefresh dataToRefresh)
        {
            dataToRefreshList.Add(dataToRefresh);           
        }

        private void RefreshData(DataToRefresh[] data)
        {
            foreach (var dataToRefresh in dataToRefreshList.Where(x => data.Contains(x)))
            {
                switch (dataToRefresh)
                {
                    case DataToRefresh.OutgoesList:
                        ((OutgoesViewModel)outgoesViewModel).RefreshListOfOutgoes();
                        break;
                    case DataToRefresh.IncomeList:
                        ((IncomeViewModel)incomeViewModel).RefreshListOfIncome();
                        break;
                    case DataToRefresh.SavingsAccountsStates:
                        ((SavingsViewModel)savingsViewModel).RefreshAccountsState();
                        break;
                }
            }

            dataToRefreshList.RemoveWhere(x => data.Contains(x));
        }

        private void ChangePage(string name)
        {
            switch (name)
            {
                case "Home":
                    CurrentPage = homeViewModel;
                    break;
                case "Outgoes":
                    CurrentPage = outgoesViewModel;
                    RefreshData(new[] { DataToRefresh.OutgoesList });
                    //outgoesViewModel.Refresh();
                    break;
                case "Income":
                    CurrentPage = incomeViewModel;
                    RefreshData(new[] { DataToRefresh.IncomeList });
                    //incomeViewModel.Refresh();
                    break;
                case "Savings":
                    CurrentPage = savingsViewModel;
                    RefreshData(new[] { DataToRefresh.SavingsAccountsStates });
                    //savingsViewModel.Refresh();
                    break;
                case "CheckPoint":
                    CurrentPage = checkPointViewModel;
                    break;
                case "Counter":
                    CurrentPage = counterViewModel;
                    break;
                case "CyclePayments":
                    CurrentPage = cyclePaymentsViewModel;
                    break;
                case "SettlementPeriod":
                    CurrentPage = settlementPeriodViewModel;
                    break;
                default:
                    break;
            }
        }

        IPageViewModel homeViewModel;
        IPageViewModel outgoesViewModel;
        IPageViewModel incomeViewModel;
        IPageViewModel savingsViewModel;
        IPageViewModel checkPointViewModel;
        IPageViewModel counterViewModel;
        IPageViewModel cyclePaymentsViewModel;
        IPageViewModel settlementPeriodViewModel;



        public string Title { get; set; } = "Home Budget";

        private IPageViewModel currentPage;

        public IPageViewModel CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                Notify();
            }
        }

        ICommand PageSwitcher;

        public ICommand SwitchPage
        {
            get
            {
                return PageSwitcher;
            }
        }




    }
}
