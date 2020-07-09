using HomeBudget.DataAccess;
using HomeBudget.Service.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Facade
{
    public class PaymentFacade : IPaymentFacade
    {
        private ICyclePaymentServices cyclePaymentServ;
        private ICounterServices CounterServ;
        private ICommonTransactionsServices commonTransactionServ;

        public PaymentFacade(ICyclePaymentServices cyclePaymentServices, ICounterServices counterServices, ICommonTransactionsServices commonTransactionsServices)
        {
            cyclePaymentServ = cyclePaymentServices;
            CounterServ = counterServices;
            commonTransactionServ = commonTransactionsServices;
        }

        #region CyclePayments

        public void PayCyclePayments(int billId, int settlementperiodId, int sourceAccountGroupId)
        {
            int transactionId;

            var billList = cyclePaymentServ.GetCyclePayments(settlementperiodId);
            
            var bill = billList.SingleOrDefault(x => x.Id == billId);

            if(bill == null)
            {
                throw new ObjectNotFoundException("Can not found bill with this Id");
            }

            if(bill.DestinationAccountGroupId != null)
            {
                var transactionsId = commonTransactionServ.DoInternalTransfer(bill.Name, bill.Amount, sourceAccountGroupId, (int)bill.DestinationAccountGroupId, settlementperiodId);
                transactionId = transactionsId[0];
                cyclePaymentServ.SaveTransactionIncomeId(billId, transactionsId[1]);
            }
            else
            {
                transactionId = commonTransactionServ.AddOutgoes(bill.Name, bill.Amount, sourceAccountGroupId, settlementperiodId);
            }

            cyclePaymentServ.SaveTransactionOutgoesId(billId, transactionId);
        }

        public void NotPayCyclePayments(int cyclePaymentsId)
        {
            cyclePaymentServ.NotPayCyclePayemnts(cyclePaymentsId);
        }

        #endregion CyclePayments

        #region Counters

        public ICalculatedCounter CalculatePaymentForCounter(int counterId, List<Counter> listOfActualCounters, int settlementPeriodPreviousId)
        {
            var actualCounterList = listOfActualCounters;
            var previousCounterList = CounterServ.GetCounters(settlementPeriodPreviousId);


            ICounterCalculator counterCalculator = new CounterCalculator(previousCounterList, actualCounterList);
            var listOfCalculatedCounters = counterCalculator.GetCalculatedCounters();

            return listOfCalculatedCounters.SingleOrDefault(x => x.Id == counterId);
        }

        public ICalculatedCounter CalculatePaymentForCounter(int counterId, int settlementperiodId, int settlementPeriodPreviousId)
        {
            var actualCounterList = CounterServ.GetCounters(settlementperiodId).Where(x => x.Id == counterId).ToList();
            var previousCounterList = CounterServ.GetCounters(settlementPeriodPreviousId);


            ICounterCalculator counterCalculator = new CounterCalculator(previousCounterList, actualCounterList);
            var listOfCalculatedCounters = counterCalculator.GetCalculatedCounters();

            return listOfCalculatedCounters.SingleOrDefault(x => x.Id == counterId);
        }

        public void SaveCounterAmount(int id, decimal counterAmount)
        {
            CounterServ.ChangeCounterAmount(id, counterAmount);
        }

        public void PayCounter(int counterId, int settlementperiodId, int settlementPeriodPreviousId, int sourceAccountGroupId)
        {
            var actualCounterList = CounterServ.GetCounters(settlementperiodId);
            var counter = actualCounterList.SingleOrDefault(x => x.Id == counterId);

            var calculatedCounter = CalculatePaymentForCounter(counterId, settlementperiodId, settlementPeriodPreviousId);

            if (counter == null || calculatedCounter == null)
            {
                throw new ObjectNotFoundException("Can not found counter with this Id");
            }

            if (counter.DestinationAccountGroupId != null)
            {

                var transactionsId = commonTransactionServ.DoInternalTransfer(counter.Name, calculatedCounter.Surcharge, sourceAccountGroupId, (int)counter.DestinationAccountGroupId, settlementperiodId);
                CounterServ.PayEqualized(counterId, transactionsId[0], transactionsId[1]);
            }
            else
            {
                int transactionId = commonTransactionServ.AddOutgoes(counter.Name, calculatedCounter.Surcharge, sourceAccountGroupId, settlementperiodId);
                CounterServ.PayEqualized(counterId, transactionId);
            }

        }

        public void NotPayCounter(int idCounter)
        {
            CounterServ.NotPayEqualized(idCounter);
        }
        #endregion Counters


    }
}
