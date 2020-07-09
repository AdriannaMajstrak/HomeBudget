using HomeBudget.Client.Model;
using HomeBudget.Client.Utilities;
using HomeBudget.DataAccess;
using HomeBudget.Service;
using HomeBudget.Service.Exceptions;
using HomeBudget.Service.Facade;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeBudget.Client.ViewModel
{
    public class CyclePaymentsViewModel : ViewModelBase, IPageViewModel
    {
        public event EventHandler<Model.DataToRefresh> RefreshData;

        #region private fields
        private ObservableCollection<Model.CyclePayment> cyclePayments;
        private ICollection<DataAccess.CyclePayment> cyclePaymentsFromDB;
        private CyclePaymentServices cyclePaymentServices;
        private CommonTransactionsServices commonTransactionsServices;
        string cyclePaymentName;
        decimal cyclePaymentAmount;
        private SettlementPeriod selctedSettlementPeriod;
        private ObservableCollection<SettlementPeriod> listOfSettlementPeriod;
        private bool ifItPayed;
        private bool canChangeAmount;
        private bool canSave;
        private Model.CyclePayment chosenCyclePayment;
        private Message messageBox;
        private PaymentFacade paymentFacade;
         
        #endregion private fields

        public CyclePaymentsViewModel(BudgetEntities budgetEntities)
        {
            cyclePaymentServices = new CyclePaymentServices(budgetEntities);

            commonTransactionsServices = new CommonTransactionsServices(budgetEntities);

            GetListOfCyclePayments();
            
            ListOfSettlementPeriod = new ObservableCollection<SettlementPeriod>((new SettlementPeriodServices(budgetEntities)).GetListOfSettlementPeriods());

            selctedSettlementPeriod = listOfSettlementPeriod.First(x => x.Id == Properties.Settings.Default.ActualSettlementPeriodId);

            paymentFacade = new PaymentFacade(cyclePaymentServices, new CounterServices(budgetEntities), new CommonTransactionsServices(budgetEntities));

            messageBox = new Message();
        }

        #region propertis
        public ObservableCollection<Model.CyclePayment> CyclePayments
        {
            get
            {
                return cyclePayments;
            }
            set
            {
                cyclePayments = value;
                Notify();
            }
        }

        public string CyclePaymentName
        {
            get
            {
                return cyclePaymentName;
            }
            set
            {
                cyclePaymentName = value;
                Notify();
                
            }
        }

        public decimal CyclePaymentAmount
        {
            get
            {
                return cyclePaymentAmount;
            }
            set
            {
                cyclePaymentAmount = value;
                Notify();
            }
        }

        public SettlementPeriod SelctedSettlementPeriod
        {
            get
            {
                return selctedSettlementPeriod;
            }
            set
            {
                selctedSettlementPeriod = value;
                Notify();
            }
        }

        public ObservableCollection<SettlementPeriod> ListOfSettlementPeriod
        {
            get
            {
                return listOfSettlementPeriod;
            }
            set
            {
                listOfSettlementPeriod = value;
                Notify();
            }
        }

        public bool IfItPayed
        {
            get
            {
                return ifItPayed;
            }
            set
            {
                ifItPayed = value;
                CanChangeAmount = !ifItPayed;
                Notify();
            }
        }

        public bool CanChangeAmount
        {
            get
            {
                return canChangeAmount;
            }
            set
            {
                canChangeAmount = value;
                Notify();
            }
        }

        public Model.CyclePayment ChosenCyclePayment
        {
            get
            {
                return chosenCyclePayment;
            }
            set
            {
                chosenCyclePayment = value;
                Notify();
                changeSelectedCyclePayment();
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

        public bool CanSave
        {
            get
            {
                return canSave;
            }
            set
            {
                canSave = value;
                Notify();
            }

        }
        #endregion propertis

        #region public methods



        //Account Group Id 1 bo z bierzacego konta)
        public ICommand SaveChanges
        {
            
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {

                        if (IfItPayed)
                        {
                            if(ChosenCyclePayment.TransactionOutgoesId != null)
                            {
                                cyclePaymentServices.ChangeCyclePaymentAmount(ChosenCyclePayment.Id, CyclePaymentAmount);
                                commonTransactionsServices.EditOutgoes((int)ChosenCyclePayment.TransactionOutgoesId, ChosenCyclePayment.Name,
                                    CyclePaymentAmount, 1, ChosenCyclePayment.SettlementPeriodId);

                                if (ChosenCyclePayment.TransactionIncomeId != null)
                                {
                                    commonTransactionsServices.EditIncome((int)ChosenCyclePayment.TransactionIncomeId, ChosenCyclePayment.Name,
                                        CyclePaymentAmount, (int)ChosenCyclePayment.DestinationAccountGroupId, ChosenCyclePayment.SettlementPeriodId);
                                }

                            }
                            else
                            {
                                cyclePaymentServices.ChangeCyclePaymentAmount(ChosenCyclePayment.Id, CyclePaymentAmount);
                                paymentFacade.PayCyclePayments(ChosenCyclePayment.Id, SelctedSettlementPeriod.Id, 1);
                            }
                        }
                        else
                        {
                            cyclePaymentServices.ChangeCyclePaymentAmount(ChosenCyclePayment.Id, CyclePaymentAmount);
                            paymentFacade.NotPayCyclePayments(ChosenCyclePayment.Id);
                        }

                        MessageBox.Visibility = true;
                        MessageBox.Color = 1;
                        MessageBox.MessageContent = "Zapisano";

                        Task.Factory.StartNew(() =>
                        {
                            Thread.Sleep(2000);
                            MessageBox.Visibility = false;
                        });

                        GetListOfCyclePayments();
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

                    RefreshData?.Invoke(this, Model.DataToRefresh.SavingsAccountsStates);
                    RefreshData?.Invoke(this, Model.DataToRefresh.OutgoesList);
                    RefreshData?.Invoke(this, Model.DataToRefresh.IncomeList);
                });
                
            }

        }
        #endregion public methods

        private void changeSelectedCyclePayment()
        {
            if (chosenCyclePayment != null)
            {
                CyclePaymentName = ChosenCyclePayment.Name;
                CyclePaymentAmount = ChosenCyclePayment.Amount;
                IfItPayed = ChosenCyclePayment.TransactionOutgoesId != null ? true : false;
                CanSave = true;
            }
            else
            {
                CyclePaymentName = string.Empty;
                CyclePaymentAmount = (decimal)0;
                IfItPayed = false;
                CanSave = false;
            }
           
        }

        private void GetListOfCyclePayments()
        {
            cyclePaymentsFromDB = cyclePaymentServices.GetCyclePayments(Properties.Settings.Default.ActualSettlementPeriodId);

            var cyclePaymentsModel = cyclePaymentsFromDB.Select(x =>
            new Model.CyclePayment()
            {
                Id = x.Id,
                SettlementPeriodId = x.SettlementPeriodId,
                Name = x.Name,
                Amount = x.Amount,
                TransactionIncomeId = x.TransactionIncomeId,
                TransactionOutgoesId = x.TransactionOutgoesId,
                DestinationAccountGroupId = x.DestinationAccountGroupId
            });

            CyclePayments = new ObservableCollection<Model.CyclePayment>(cyclePaymentsModel);
        }
    }
}
