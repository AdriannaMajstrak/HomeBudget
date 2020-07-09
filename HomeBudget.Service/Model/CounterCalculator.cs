using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HomeBudget.Test")]

namespace HomeBudget.Service.Model
{


    internal class CounterCalculator : ICounterCalculator
    {
        ICollection<Counter> listOfActualCounters;
        ICollection<Counter> listOfPreviousCounters;

        public CounterCalculator(ICollection<Counter> listOfPreviousCounters, ICollection<Counter> listOfActualCounters)
        {
            this.listOfActualCounters = listOfActualCounters;
            this.listOfPreviousCounters = listOfPreviousCounters;
        }

        public ICollection<CalculatedCounter> GetCalculatedCounters()
        {

            var listActualCalculatedCounter = listOfActualCounters
                .Select(x => new CalculatedCounter
                {
                    Id = x.Id,
                    AccountGroup = x.AccountGroup,
                    AmountCounter = x.AmountCounter,
                    DestinationAccountGroupId = x.DestinationAccountGroupId,
                    Equalized = x.Equalized,
                    ForecastIncrease = x.ForecastIncrease,
                    Name = x.Name,
                    Rate = x.Rate,
                    SettlementPeriodId = x.SettlementPeriodId,
                    TransactionIncomeId = x.TransactionIncomeId,
                    TransactionOutgoesId = x.TransactionOutgoesId,
                    Equalizable = x.Equalizable

                }).ToList();

            for (int i = 0; i < listActualCalculatedCounter.Count; i++)
            {
               
                if (listActualCalculatedCounter[i].ForecastIncrease != null)
                {
                    var lastEqualent = listOfPreviousCounters.FirstOrDefault(x => x.Name == listActualCalculatedCounter[i].Name);

                    if (lastEqualent != null)
                    {
                        listActualCalculatedCounter[i].Increase = listActualCalculatedCounter[i].AmountCounter - lastEqualent.AmountCounter;
                    }
                    else
                    {
                        listActualCalculatedCounter[i].Increase = listActualCalculatedCounter[i].AmountCounter;
                    }

                    if (listActualCalculatedCounter[i].Equalizable == true)
                    {
                        listActualCalculatedCounter[i].Surcharge = (listActualCalculatedCounter[i].Increase - (decimal)listActualCalculatedCounter[i].ForecastIncrease) * (decimal)listActualCalculatedCounter[i].Rate;
                        if(listActualCalculatedCounter[i].Surcharge < 0)
                        {
                            listActualCalculatedCounter[i].Surcharge = 0;
                        }
                    }
                }
                else
                {
                    var lastEqualent = listOfPreviousCounters.FirstOrDefault(x => x.Name == listActualCalculatedCounter[i].Name);

                    if (lastEqualent != null)
                    {
                        listActualCalculatedCounter[i].Increase = listActualCalculatedCounter[i].AmountCounter - lastEqualent.AmountCounter;
                    }
                    else
                    {
                        listActualCalculatedCounter[i].Increase = listActualCalculatedCounter[i].AmountCounter;
                    }

                    if (listActualCalculatedCounter[i].Equalizable == true)
                    {
                        listActualCalculatedCounter[i].Surcharge = (listActualCalculatedCounter[i].Increase) * (decimal)listActualCalculatedCounter[i].Rate;

                        if (listActualCalculatedCounter[i].Surcharge < 0)
                        {
                            listActualCalculatedCounter[i].Surcharge = 0;
                        }
                    }
                }
            }
            return listActualCalculatedCounter;
        }
    }

    
}
