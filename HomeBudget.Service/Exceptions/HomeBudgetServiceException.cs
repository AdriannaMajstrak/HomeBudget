using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Exceptions
{
    public class HomeBudgetServiceException : Exception
    {
        public HomeBudgetServiceException()
        {
        }

        public HomeBudgetServiceException(string message)
            : base(message)
        {
        }

        public HomeBudgetServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
