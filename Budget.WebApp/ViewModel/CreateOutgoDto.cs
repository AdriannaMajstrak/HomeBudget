using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.WebApp.ViewModel
{
    public class CreateOutgoDto
    {
        public int SettlementPeriodId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}