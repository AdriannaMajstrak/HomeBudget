using HomeBudget.Client.Model;
using HomeBudget.Client.Utilities;
using HomeBudget.DataAccess;
using HomeBudget.Service;
using HomeBudget.Service.Exceptions;
using HomeBudget.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeBudget.Client.ViewModel
{
    public class SavingsViewModel : ViewModelBase, IPageViewModel
    {
        private IAccountServices accountServices;
        private ICommonTransactionsServices commonTransactionServices;
        private ICollection<AccountGroup> listAccountFrom;
        private ICollection<AccountGroup> listAccountTo;
        private AccountGroup chosenAccountFrom;
        private AccountGroup chosenAccountTo;
        private string title;
        private decimal amount;
        private Message messageBox;

        private decimal commonAccountState;
        private decimal cyclePaymentsAccountState;
        private decimal studiesAccountState;
        private decimal savingsAccountState;

        public event EventHandler<DataToRefresh> RefreshData;

        public SavingsViewModel(BudgetEntities budgetEntities)
        {
            accountServices = new AccountServices(budgetEntities);
            commonTransactionServices = new CommonTransactionsServices(budgetEntities);
            ListAccountFrom = accountServices.GetAllAccountGroups();
            ListAccountTo = accountServices.GetAllAccountGroups();
            MessageBox = new Message();
            MessageBox.Visibility = false;
            RefreshAccountsState();

            // powinien pobierac bierzacuySettlement perioda jako paramentr
        }

       

        public ICollection<AccountGroup> ListAccountFrom
        {
            get
            {
                return listAccountFrom;
            }
            set
            {
                listAccountFrom = value;
                
            }
        }

        public ICollection<AccountGroup> ListAccountTo
        {
            get
            {
                return listAccountTo;
            }
            set
            {
                listAccountTo = value;
            }
        }

        public AccountGroup ChosenAccountFrom
        {
            get
            {
                return chosenAccountFrom;
            }
            set
            {
                chosenAccountFrom = value;
            }
        }

        public AccountGroup ChosenAccountTo
        {
            get
            {
                return chosenAccountTo;
            }
            set
            {
                chosenAccountTo = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                Notify();
            }
        }

        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                Notify();
            }
        }

        public Message MessageBox
        {
            get
            {
                return messageBox;
            }
            set
            {
                messageBox = value;
                Notify();
            }
        }

        public decimal CommonAccountState
        {
            get
            {
                return commonAccountState;
            }
            set
            {
                commonAccountState = value;
                Notify();
            }
        }

        public decimal CyclePaymentsAccountState
        {
            get
            {
                return cyclePaymentsAccountState;
            }
            set
            {
                cyclePaymentsAccountState = value;
                Notify();
            }
        }

        public decimal StudiesAccountState
        {
            get
            {
                return studiesAccountState;
            }
            set
            {
                studiesAccountState = value;
                Notify();
            }
        }

        public decimal SavingsAccountState
        {
            get
            {
                return savingsAccountState;
            }
            set
            {
                savingsAccountState = value;
                Notify();
            }
        }

        public ICommand SaveInternalTransfer
        {
            get
            {

                return new RelayCommand(() =>
                {
                    try
                    {

                        var saved = commonTransactionServices.DoInternalTransfer(title, amount, chosenAccountFrom.Id, chosenAccountTo.Id, 
                            Properties.Settings.Default.ActualSettlementPeriodId);
                        MessageBox.Visibility = true;
                        MessageBox.Color = 1;
                        MessageBox.MessageContent = "Zapisano";
                        Amount = 0;
                        Title = "";

                        RefreshAccountsState();

                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(2000);
                            MessageBox.Visibility = false;
                        });
                    }

                    catch (HomeBudgetServiceException ex)
                    {
                        MessageBox.Visibility = true;
                        MessageBox.Color = 0;
                        MessageBox.MessageContent = "Nie zpaisano";
                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(2000);
                            MessageBox.Visibility = false;
                        });
                    }

                });
            }
        }

        public void RefreshAccountsState()
        {
            CommonAccountState = (new CommonTransactions(commonTransactionServices.GetCommonTransactions(1, Properties.Settings.Default.ActualSettlementPeriodId))).TransactionsDifferent;//powinien tu dawac jako drugi paramentr aktualnny settlement period
            CyclePaymentsAccountState = (new CommonTransactions(commonTransactionServices.GetCommonTransactions(3, Properties.Settings.Default.ActualSettlementPeriodId))).TransactionsDifferent;
            StudiesAccountState = (new CommonTransactions(commonTransactionServices.GetCommonTransactions(4, Properties.Settings.Default.ActualSettlementPeriodId))).TransactionsDifferent;
            SavingsAccountState = (new CommonTransactions(commonTransactionServices.GetCommonTransactions(2, Properties.Settings.Default.ActualSettlementPeriodId))).TransactionsDifferent;
        }

        

       

    }
}
