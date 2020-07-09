using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;
using HomeBudget.Service.Exceptions;


namespace HomeBudget.Service
{
    public class CyclePaymentServices : ICyclePaymentServices
    {
        private BudgetEntities dbContext;


        public CyclePaymentServices(BudgetEntities dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateCyclePaymentByPeriod(int IdSettlementPeriod)
        {
            try
            {
                foreach (var item in dbContext.CyclePaymentTemplates)
                {
                    var addedBill = new CyclePayment()
                    {
                        SettlementPeriodId = IdSettlementPeriod,
                        Name = item.Name,
                        Amount = item.Amount,
                        TransactionOutgoesId = null,
                        TransactionIncomeId =null,
                        DestinationAccountGroupId = item.AccountGroupId
                    };

                    dbContext.CyclePayments.Add(addedBill);
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public List<CyclePayment> GetCyclePayments(int idSettlementPeriod)
        {
            var ListOfBill = dbContext.CyclePayments.Where(x => x.SettlementPeriodId == idSettlementPeriod).ToList();
            return ListOfBill;
        }

        public void ChangeCyclePaymentAmount(int id, decimal newAmount)
        {
            try
            {
                var toChange = dbContext.CyclePayments.Find(id);
                toChange.Amount = newAmount;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
            
        }

        public void SaveTransactionOutgoesId(int CyclePaymentId, int TransactionId)
        {
            try
            {
                var toChange = dbContext.CyclePayments.Find(CyclePaymentId);
                toChange.TransactionOutgoesId = TransactionId;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void SaveTransactionIncomeId(int CyclePaymentId, int TransactionId)
        {
            try
            {
                var toChange = dbContext.CyclePayments.Find(CyclePaymentId);
                toChange.TransactionIncomeId= TransactionId;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void ChangeAccountGrupId(int idBill, int? newIdAccountGroup)
        {
            try
            {
                var toChange = dbContext.CyclePayments.Find(idBill);
                toChange.DestinationAccountGroupId = newIdAccountGroup;
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }

        public void NotPayCyclePayemnts(int cyclePaymentsId)
        {
            try
            {

                var toChange = dbContext.CyclePayments.Find(cyclePaymentsId);

                var tr1 = toChange.TransactionOutgoesId;
                var tr2 = toChange.TransactionIncomeId;


                if (tr1 != null)
                {
                    dbContext.Transactions.Remove(dbContext.Transactions.Find((int)tr1));
                }

                if (tr2 != null)
                {
                    dbContext.Transactions.Remove(dbContext.Transactions.Find((int)tr2));
                }

                toChange.TransactionOutgoesId = null;
                toChange.TransactionIncomeId = null;

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new HomeBudgetServiceException(string.Empty, ex);
            }
        }
    }
}
