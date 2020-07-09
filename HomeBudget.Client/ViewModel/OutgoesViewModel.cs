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
   public class OutgoesViewModel : ViewModelBase, IPageViewModel
    {
        private BudgetEntities budgetEntities;
        private string outgoName;
        private decimal outgoAmount;
        private SettlementPeriod selctedSettlementPeriodForAdd;
        private SettlementPeriod selctedSettlementPeriodForShowList;
        private Message messageBox;
        private CommonTransactions commonTransactionsModel;
        List<SettlementPeriod> listOfSettlementPeriodForAdd;
        List<SettlementPeriod> listOfSettlementPeriodForShowList;
        List<Transaction> listOfOutgoes;
        private decimal sumOfOutgoesByPeriod;
        private ICommonTransactionsServices commonTransactionServices;

        public event EventHandler<DataToRefresh> RefreshData;

        public OutgoesViewModel(BudgetEntities budgetEntities)
        {
            this.budgetEntities = budgetEntities;
            listOfSettlementPeriodForAdd = (new SettlementPeriodServices(budgetEntities)).GetListOfSettlementPeriods();
            listOfSettlementPeriodForShowList = listOfSettlementPeriodForAdd;
            selctedSettlementPeriodForAdd = listOfSettlementPeriodForAdd.SingleOrDefault(x => x.Id == Properties.Settings.Default.ActualSettlementPeriodId);
            selctedSettlementPeriodForShowList = listOfSettlementPeriodForShowList.SingleOrDefault(x => x.Id == Properties.Settings.Default.ActualSettlementPeriodId);
            commonTransactionServices = new CommonTransactionsServices(budgetEntities);
            //messageBox = new Message();
            //messageBox.Visibility = true;
            //messageBox.MessageContent = "jest";
            MessageBox = new Message();
            MessageBox.Visibility = false;
            RefreshListOfOutgoes();

        }
        
        public decimal SumOfOutgoesByPeriod
        {
            get
            {
                return sumOfOutgoesByPeriod;
            }
            set
            {
                sumOfOutgoesByPeriod = value;
                Notify();
            }
        }

        public List<Transaction> ListOfOutgoes
        {
            get
            {
                return listOfOutgoes;
            }
            set
            {
                listOfOutgoes = value;
                Notify();
            }
        }

        public SettlementPeriod SelectedSettlemenrPeriodForAdd
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

        public SettlementPeriod SelectedSettlementPeriodForShowList
        {
            get
            {
                return selctedSettlementPeriodForShowList;
            }
            set
            {
                selctedSettlementPeriodForShowList = value;
                Notify();
                RefreshListOfOutgoes();
            }
        }

        public List<SettlementPeriod> ListOfSettlementPeriodForAdd
        {
            get
            {
                return listOfSettlementPeriodForAdd;
            }
        }

        public List<SettlementPeriod> ListOfSettlementPeriodForShowList
        {
            get
            {
                return listOfSettlementPeriodForShowList;
            }
            set
            {
                listOfSettlementPeriodForShowList = value;
                Notify();
            }
        }

        public string OutgoName
        {
            get
            {
                return outgoName;
            }
            set
            {
                outgoName = value;
                Notify();
            }
        }

        public decimal OutgoAmount
        {
            get
            {
                return outgoAmount;
            }
            set
            {
                outgoAmount = value;
                Notify();
            }
        }
        
        public ICommand SaveNewOutgo
        {
            get
            {

                return new RelayCommand(() =>
                {
                    try
                    {
                        var saved = commonTransactionServices.AddOutgoes(outgoName, outgoAmount, 1, SelectedSettlemenrPeriodForAdd.Id);
                        MessageBox.Visibility = true;
                        MessageBox.Color= 1;
                        MessageBox.MessageContent = "Zapisano";
                        OutgoAmount = 0;
                        OutgoName = "";
                        RefreshListOfOutgoes();
                        

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

        private void GetSumOfOutgoesByPeriod()
        {
            if (commonTransactionsModel == null)
            {
                commonTransactionsModel = new CommonTransactions(commonTransactionServices.GetCommonTransactions(1, SelectedSettlementPeriodForShowList.Id));
            }
            else
            {
                commonTransactionsModel.ListOfAllTransactions = commonTransactionServices.GetCommonTransactions(1, SelectedSettlementPeriodForShowList.Id);
            }

            SumOfOutgoesByPeriod = commonTransactionsModel.SumOfOutgoes;

        }

        //1 - id grupy kont z których płacimy biezace wydatki
        public void RefreshListOfOutgoes()
        {
            if (commonTransactionsModel == null)
            {
                commonTransactionsModel = new CommonTransactions(commonTransactionServices.GetCommonTransactions(1, SelectedSettlementPeriodForShowList.Id));
            }
            else
            {
                commonTransactionsModel.ListOfAllTransactions = commonTransactionServices.GetCommonTransactions(1, SelectedSettlementPeriodForShowList.Id);
            }
            ListOfOutgoes = (List<Transaction>)commonTransactionsModel.ListOfOutgoes;
            GetSumOfOutgoesByPeriod();
        }
   }

}

