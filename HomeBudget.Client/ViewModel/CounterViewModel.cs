
using HomeBudget.Service;
using HomeBudget.Service.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.Client.Utilities;
using System.Collections.ObjectModel;
using HomeBudget.DataAccess;
using System.Windows.Input;
using System.Threading;
using HomeBudget.Service.Exceptions;


namespace HomeBudget.Client.ViewModel
{
    public class CounterViewModel : ViewModelBase, IPageViewModel
    {
        private ICounterServices counterServices;
        private ObservableCollection<Model.Counter> listOfActualCounters;
        private List<Counter> listOfPreviousCounters;
        private ICollection<Model.Counter> originListOfModelCounters;
        IPaymentFacade paymentFacade;
        ISettlementPeriodServices settlementPeriodServices;
        List<Counter> countersFromDb;
        private Model.Message messageBox = new Model.Message();
        private int actualSettlementPeriodId;
        private int previousSettlementPeriodId;
        private int sourceAccountGroupId = 1;//1-to indeks grupy kont biezace, wpisany na sztywno poniewaz zwykle wyrównanie spłacamy z biezacych pieniedzy
        private SettlementPeriod selctedSettlementPeriodForShowList;
        private List<SettlementPeriod> listOfSettlementPeriodForShowList;
      

        public event EventHandler<Model.DataToRefresh> RefreshData;

        public CounterViewModel(BudgetEntities budgetEntities)
        {
            counterServices = new CounterServices(budgetEntities);
            settlementPeriodServices = new SettlementPeriodServices(budgetEntities);

            actualSettlementPeriodId = Properties.Settings.Default.ActualSettlementPeriodId;
            previousSettlementPeriodId = settlementPeriodServices.GetSettlementPeriodIdbyDate(
                    settlementPeriodServices.GetSettlementPeriodDateById(Properties.Settings.Default.ActualSettlementPeriodId).AddMonths(-1));

            ListOfSettlementPeriodForShowList = settlementPeriodServices.GetListOfSettlementPeriods();
            SelctedSettlementPeriodForShowList = ListOfSettlementPeriodForShowList.First(x => x.Id == previousSettlementPeriodId);


            countersFromDb = counterServices.GetCounters(actualSettlementPeriodId);

            var countersModel = countersFromDb.Select(x =>
                new Model.Counter()
                {
                    Name = x.Name,
                    AmountCounter = x.AmountCounter,
                    Id = x.Id,
                    Equalized = x.Equalized,
                    Equalizable = (bool)x.Equalizable
                });

            ListOfActualCounters = new ObservableCollection<Model.Counter>(countersModel);


            originListOfModelCounters = ListOfActualCounters.Select(x =>
            new Model.Counter()
            {
                Name = x.Name,
                AmountCounter = x.AmountCounter,
                Id = x.Id,
                Equalized = x.Equalized,
                Equalizable = (bool)x.Equalizable

            }).ToList();

            paymentFacade = new PaymentFacade(new CyclePaymentServices(budgetEntities), counterServices, new CommonTransactionsServices(budgetEntities));

            foreach (var counter in ListOfActualCounters)
            {
                counter.AmountChanged += CalculateIncrease;
                this.CalculateIncrease(counter, counter.AmountCounter);
            }

            RefreshListOfCounters();
        }

        public ObservableCollection<Model.Counter> ListOfActualCounters
        {
            get
            {
                return listOfActualCounters;
            }
            set
            {
                listOfActualCounters = value;
                Notify();
            }
        }

        public List<Counter> ListOfPreviousCounters
        {
            get
            {
                return listOfPreviousCounters;
            }
            set
            {
                listOfPreviousCounters = value;
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
                RefreshListOfCounters();
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

        public Model.Message MessageBox
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

        public ICommand SaveInDataBase
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        SaveChangedValues();

                        PaymenForCounter();

                        MessageBox.Visibility = true;
                        MessageBox.Color = 1;
                        MessageBox.MessageContent = "Zapisano";

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

        private void CalculateIncrease(object sender, decimal e)
        {
            var modifiedCounter = (HomeBudget.Client.Model.Counter)sender;
            var counterFormDb = countersFromDb.First(x => x.Id == modifiedCounter.Id);
            counterFormDb.AmountCounter = e;

            var calculatedCounter = paymentFacade.CalculatePaymentForCounter
                (
                modifiedCounter.Id,
                new List<Counter>() { counterFormDb },
                previousSettlementPeriodId
                );

            modifiedCounter.Increase = calculatedCounter.Increase;
            modifiedCounter.Surcharge = calculatedCounter.Surcharge;
        }

        private void SaveChangedValues()
        {
            var changedAmount = GetChangedAmountCounter(originListOfModelCounters, ListOfActualCounters);
            SaveChangedInDatabaseChangedAmount(changedAmount);
        }
        
        private ICollection<Model.Counter> GetChangedAmountCounter(ICollection<Model.Counter> before, ICollection<Model.Counter> after)
        {
            List<Model.Counter> changed = new List<Model.Counter>();

            foreach (var item in before)
            {
                var el = after.First(x => x.Id == item.Id);
                if(el.AmountCounter != item.AmountCounter)
                {
                    changed.Add(el);
                }
            }

            return changed;
        }

        private ICollection<Model.Counter> GetChangedEqualized(ICollection<Model.Counter> before, ICollection<Model.Counter> after)
        {
            List<Model.Counter> changed = new List<Model.Counter>();

            foreach (var item in before)
            {
                var el = after.First(x => x.Id == item.Id);
                if (el.Equalized != item.Equalized)
                {
                    changed.Add(el);
                }
            }

            return changed;
        }

        private void SaveChangedInDatabaseChangedAmount(ICollection<Model.Counter> countersWithChangedAmount)
        {
            foreach (var item in countersWithChangedAmount)
            {
                paymentFacade.SaveCounterAmount(item.Id, item.AmountCounter);
                originListOfModelCounters.First(x => x.Id == item.Id).AmountCounter = item.AmountCounter;
            }
        }

        private void PaymenForCounter()
        {
            var changedEqualized = GetChangedEqualized(originListOfModelCounters, ListOfActualCounters);

            foreach (var counter in changedEqualized)
            {
                if(counter.Equalized)
                {
                    paymentFacade.PayCounter(counter.Id, actualSettlementPeriodId, previousSettlementPeriodId, sourceAccountGroupId);
                }
                else
                {
                    paymentFacade.NotPayCounter(counter.Id);
                }

                originListOfModelCounters.First(x => x.Id == counter.Id).Equalized = counter.Equalized;

            }

            RefreshData?.Invoke(this, Model.DataToRefresh.SavingsAccountsStates);
            RefreshData?.Invoke(this, Model.DataToRefresh.OutgoesList);
            RefreshData?.Invoke(this, Model.DataToRefresh.IncomeList);


        }

        private void RefreshListOfCounters()
        {
            ListOfPreviousCounters = counterServices.GetCounters(SelctedSettlementPeriodForShowList.Id); 
        }

    }
}
