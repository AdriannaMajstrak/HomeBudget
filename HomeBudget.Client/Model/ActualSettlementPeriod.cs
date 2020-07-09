using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.Model
{
    class ActualSettlementPeriod
    {
        public ActualSettlementPeriod(int id, DateTime date)
        {
            Id = id;
            Date = date;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
