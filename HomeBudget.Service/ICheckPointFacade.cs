using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;

namespace HomeBudget.Service
{
    interface ICheckPointFacade
    {
        decimal SumMoneyOnAccountGroup(int idCheckPoint, int idAccountGroup);

        //decimal GetDifferentActualAndExpectedAmount(decimal realAmount, decimal expectedAmount);

    }
}
