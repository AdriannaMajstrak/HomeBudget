using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Model
{
    interface ICounterCalculator
    {
        ICollection<CalculatedCounter> GetCalculatedCounters();

    }
}
