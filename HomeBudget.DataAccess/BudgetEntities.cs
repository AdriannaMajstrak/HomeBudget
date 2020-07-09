using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.DataAccess
{
    public partial class BudgetEntities
    {
        public BudgetEntities(DbConnection connection)
           : base(connection, true)
        {
        }
    }
}
