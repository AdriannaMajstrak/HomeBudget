using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service
{
    public class AccountServices : IAccountServices
    {
        private BudgetEntities dbContext;

        public AccountServices(BudgetEntities db)
        {
            dbContext = db;
        }

        public List<AccountGroup> GetAllAccountGroups()
        {
            var ListOfAccountGroup = dbContext.AccountGroups.ToList();
            return ListOfAccountGroup;
        }

        public List<Account> GetAllAccounts()
        {
            var ListOfAccounts = dbContext.Accounts.ToList();
            return ListOfAccounts;
        }
    }
}
