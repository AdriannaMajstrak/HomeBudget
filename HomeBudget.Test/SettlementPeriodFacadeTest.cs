using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HomeBudget.DataAccess;
using System.Collections.Generic;
using Effort;
using HomeBudget.Service;
using System.Linq;
using Xunit;
using Moq;
using HomeBudget.Service.Facade;



namespace HomeBudget.Test
{
    public class SettlementPeriodFacadeTest
    {
        [Fact]
        public void CanCreateNewSettlementPeriod()
        {
            DateTime now = DateTime.Now;

            Mock<ISettlementPeriodServices> settlementPeriodServ = new Mock<ISettlementPeriodServices>();
            settlementPeriodServ.Setup(x => x.AddNewSettlementPeriod(now)).Returns(5);

            Mock<ICyclePaymentServices> billServ = new Mock<ICyclePaymentServices>();

            Mock<ICounterServices> counterServ = new Mock<ICounterServices>();

            SettlementPeriodFacade settPerFacade = new SettlementPeriodFacade(settlementPeriodServ.Object, billServ.Object, counterServ.Object);

           Xunit.Assert.Equal(5, settPerFacade.CreateNewSettlementPeriod(now));
        }
    }
}
