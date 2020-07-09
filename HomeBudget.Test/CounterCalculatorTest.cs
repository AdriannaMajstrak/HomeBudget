using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using Moq;
using HomeBudget.Service.Model;

namespace HomeBudget.Test
{
    public class CounterCalculatorTest
    {
        [Theory]
        [InlineData(4, 100.1)]
        [InlineData(5, 7.4)]
        [InlineData(6, 100)]
        public void CanGetCalculatedCountersCheckIncrease(int counterId, decimal incrase)
        {
            var previousCounterList = new List<Counter>
            {
                new Counter()
                {
                    Id = 1,
                    SettlementPeriodId =1,
                    Name = "electricity",
                    AmountCounter = 9856.6m,
                    Rate = 2.80m,
                    Equalized = false,
                    ForecastIncrease = null
                },
                new Counter()
                {
                    Id = 2,
                    SettlementPeriodId =1,
                    Name = "water",
                    AmountCounter = 418.6m,
                    Rate = 5m,
                    Equalized = true,
                    DestinationAccountGroupId = 2,
                    ForecastIncrease = 7

                },
                 new Counter()
                {
                    Id = 3,
                    SettlementPeriodId =1,
                    Name = "gas",
                    AmountCounter = 100.9m,
                    Rate = 5.5m,
                    Equalized = true,
                    ForecastIncrease = null

                }
            };

            var actualCounterList = new List<Counter>()
            {
                new Counter()
                {
                    Id = 4,
                    SettlementPeriodId =2,
                    Name = "electricity",
                    AmountCounter = 9956.7m,
                    Rate = 2.80m,
                    Equalized = false,
                    ForecastIncrease = null

                },
                new Counter()
                {
                    Id = 5,
                    SettlementPeriodId =2,
                    Name = "water",
                    AmountCounter = 426m,
                    Rate = 5m,
                    Equalized = true,
                    ForecastIncrease = 7

                },
                 new Counter()
                {
                    Id = 6,
                    SettlementPeriodId =2,
                    Name = "gas",
                    AmountCounter = 200.9m,
                    Rate = 5.5m,
                    Equalized = true,
                    ForecastIncrease = null

                }
            };

            CounterCalculator counterCalculator = new CounterCalculator(previousCounterList, actualCounterList);

            var listCalculatedCounters = counterCalculator.GetCalculatedCounters();

            Xunit.Assert.Equal(incrase, listCalculatedCounters.First(x => x.Id == counterId).Increase);
        }

        [Theory]
        [InlineData(4, 0)]
        [InlineData(5, 2)]
        [InlineData(6, 550)]
        public void CanGetCalculatedCountersCheckAmount(int counterId, decimal surcharge)
        {
            var previousCounterList = new List<Counter>
            {
                new Counter()
                {
                    Id = 1,
                    SettlementPeriodId =1,
                    Name = "electricity",
                    AmountCounter = 9856.6m,
                    Rate = 2.80m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable=false
                },
                new Counter()
                {
                    Id = 2,
                    SettlementPeriodId =1,
                    Name = "water",
                    AmountCounter = 418.6m,
                    Rate = 5m,
                    Equalized = true,
                    DestinationAccountGroupId = 2,
                    ForecastIncrease = 7,
                    Equalizable=true


                },
                 new Counter()
                {
                    Id = 3,
                    SettlementPeriodId =1,
                    Name = "gas",
                    AmountCounter = 100.9m,
                    Rate = 5.5m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable=true                 

                }
            };

            var actualCounterList = new List<Counter>()
            {
                new Counter()
                {
                    Id = 4,
                    SettlementPeriodId =2,
                    Name = "electricity",
                    AmountCounter = 9956.7m,
                    Rate = 2.80m,
                    Equalized = true,
                    ForecastIncrease = null,
                    Equalizable=false

                },
                new Counter()
                {
                    Id = 5,
                    SettlementPeriodId =2,
                    Name = "water",
                    AmountCounter = 426m,
                    Rate = 5m,
                    Equalized = false,
                    ForecastIncrease = 7,
                    Equalizable=true
                },
                 new Counter()
                {
                    Id = 6,
                    SettlementPeriodId =2,
                    Name = "gas",
                    AmountCounter = 200.9m,
                    Rate = 5.5m,
                    Equalized = false,
                    Equalizable=true,
                    ForecastIncrease = null
                }
            };

            CounterCalculator counterCalculator = new CounterCalculator(previousCounterList, actualCounterList);

            var listCalculatedCounters = counterCalculator.GetCalculatedCounters();

            Xunit.Assert.Equal(surcharge, listCalculatedCounters.First(x => x.Id == counterId).Surcharge);
        }
    }
    
}
