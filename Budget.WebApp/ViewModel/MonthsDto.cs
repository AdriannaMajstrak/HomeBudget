using System;
using System.Linq;
using System.Web;
using HomeBudget.DataAccess;
using HomeBudget.Service;

namespace Budget.WebApp.ViewModel
{
    public class MonthsDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
    }
}