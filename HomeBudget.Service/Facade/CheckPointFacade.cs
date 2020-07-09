using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;

namespace HomeBudget.Service.Facade
{
    public class CheckPointFacade : ICheckPointFacade
    {
        private CheckPointServices checkPointServices;

        public CheckPointFacade(CheckPointServices checkPointServ)
        {
            checkPointServices = checkPointServ;
        }

        //public decimal GetDifferentActualAndExpectedAmount(decimal realAmount, decimal expectedAmount)
        //{
        //    return realAmount-expectedAmount;
        //}

        //Nie wiem ktore rozwiazanie lepsze pierwsze:
        //public decimal SumMoneyOnAccountGroup(List<CheckPointEntry> listOfChPointEntry, int idAccountGroup)
        //{
        //    decimal sum = 0;

        //    foreach (var ChPEntry in listOfChPointEntry)
        //    {
        //        if(ChPEntry.Account.GroupId==idAccountGroup)
        //        {
        //            sum += ChPEntry.Amount;
        //        }
        //    }

        //    return sum; 
        //}

        //czy drugie:(przy czym przy drugim musze w konstruktorze stworzyc obiekt typuCheckPontServices)
        public decimal SumMoneyOnAccountGroup(int idCheckPoint, int idAccountGroup)
        {
            
            var listOfChPointEntry = checkPointServices.GetListOfCheckPointEntry(idCheckPoint);

            decimal sum = listOfChPointEntry.Where(x => x.AccountGroupId == idAccountGroup).Sum(x => x.Amount);

            //foreach (var ChPEntry in listOfChPointEntry)
            //{
            //    if (ChPEntry.Account.GroupId == idAccountGroup)
            //    {
            //        sum += ChPEntry.Amount;
            //    }
            //}

            return sum;
        }
    }
}
