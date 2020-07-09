using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Service.Model
{
    public class CalculatedCounter : DataAccess.Counter, ICalculatedCounter
    {

        public decimal Surcharge { get; set; }

        public decimal Increase { get; set; }
    }
}
