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
    public class IncomeViewModel : ViewModelBase, IPageViewModel
    {
        private BudgetEntities budgetEntities;
        private ICommonTransactionsServices commonTransactionServices;
        private string incomeName;
        private decimal incomeAmount;
        private SettlementPeriod selctedSettlementPeriodForAdd;
        private SettlementPeriod selctedSettlementPeriodForShowList;
        private Message messageBox;
        ICollection<SettlementPeriod> listOfSettlementPeriodForAdd;
        ICollection<SettlementPeriod> listOfSettlementPeriodShowList;
        private CommonTransactions commonTransactionsModel;
        private decimal sumOfIncomeByPeriod;
        List<Transaction> listOfIncome;

        public event EventHandler<DataToRefresh> RefreshData;

        public IncomeViewModel(BudgetEntities budgetEntities)
        {
            this.budgetEntities = budgetEntities;
            commonTransactionServices = new CommonTransactionsServices(budgetEntities);
            listOfSettlementPeriodForAdd = (new SettlementPeriodServices(budgetEntities)).GetListOfSettlementPeriods();
            listOfSettlementPeriodShowList = listOfSettlementPeriodForAdd;
            selctedSettlementPeriodForAdd = listOfSettlementPeriodForAdd.SingleOrDefault(x => x.Id == Properties.Settings.Default.ActualSettlementPeriodId);
            selctedSettlementPeriodForShowList = listOfSettlementPeriodForAdd.SingleOrDefault(x => x.Id == Properties.Settings.Default.ActualSettlementPeriodId);
            MessageBox = new Message();
            MessageBox.Visibility = false;
            RefreshListOfIncome();
        }

        public decimal SumOfIncomeByPeriod
        {
            get
            {
                return sumOfIncomeByPeriod;
            }
            set
            {
               sumOfIncomeByPeriod=value;
                Notify();
            }
        }

        public List<Transaction> ListOfIncome
        {
            get
            {
                return listOfIncome;
            }
            set
            {
                listOfIncome = value;
                Notify();
            }
        }

        public ICollection<SettlementPeriod> ListOfSettlementPeriodForAdd
        {
            get
            {
                return listOfSettlementPeriodForAdd;
            }
        }

        public ICollection<SettlementPeriod> ListOfSettlementPeriodShowList
        {
            get
            {
                return listOfSettlementPeriodShowList;
            }
        }

        public SettlementPeriod SelctedSettlementPeriodForAdd
        {
            get
            {
                return selctedSettlementPeriodForAdd;
            }
            set
            {
                selctedSettlementPeriodForAdd = value;
                Notify();
            }
        }

        public SettlementPeriod SelctedSettlementPeriodForShowList
        {
            get
            {
                return selctedSettlementPeriodForShowList;
            }
            set
            {
                selctedSettlementPeriodForShowList = value;
                Notify();
                RefreshListOfIncome();
            }
        }

        public string IncomeName
        {
            get
            {
                return incomeName;
            }
            set
            {
                incomeName = value;
                Notify();
            }
        }

        public decimal IncomeAmount
        {
            get
            {
                return incomeAmount;
            }
            set
            {
                incomeAmount = value;
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

        public ICommand SaveNewIncome
        {
            get
            {

                return new RelayCommand(() =>
                {
                    try
                    {
                        var saved = commonTransactionServices.AddIncome(incomeName, incomeAmount, 1, SelctedSettlementPeriodForAdd.Id);
                        MessageBox.Visibility = true;
                        MessageBox.Color = 1;
                        MessageBox.MessageContent = "Zapisano";
                        IncomeAmount = 0;
                        IncomeName = "";
                        RefreshListOfIncome();

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

        //1 - id grupy kont z których płacimy biezace wydatki
        public void RefreshListOfIncome()
        {
            commonTransactionsModel = new CommonTransactions(commonTransactionServices.GetCommonTransactions(1, SelctedSettlementPeriodForShowList.Id));
            ListOfIncome = (List<Transaction>)commonTransactionsModel.ListOfIncome;
            GetSumOfIncomeByPeriod();
        }

        private void GetSumOfIncomeByPeriod()
        {
            if (commonTransactionsModel == null)
            {
                commonTransactionsModel = new CommonTransactions(commonTransactionServices.GetCommonTransactions(1, SelctedSettlementPeriodForShowList.Id));
            }
            else
            {
                commonTransactionsModel.ListOfAllTransactions = commonTransactionServices.GetCommonTransactions(1, SelctedSettlementPeriodForShowList.Id);
            }

            SumOfIncomeByPeriod = commonTransactionsModel.SumOfIncome;
        }

    }
}
