using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Budget.WebApp.Models;
using Budget.WebApp.ViewModel;
using HomeBudget.DataAccess;
using HomeBudget.Service;
using Newtonsoft.Json.Linq;

namespace Budget.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private BudgetEntities db;
        private ICommonTransactionsServices transactionServices;
        private ISettlementPeriodServices settlementPeriodServices;
        MonthsListDto monthListDto;

        public HomeController()
        {
            db = new BudgetEntities();
            transactionServices = new CommonTransactionsServices(db);
            settlementPeriodServices = new SettlementPeriodServices(db);
            monthListDto = new MonthsListDto();
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Incomes()
        {
            return View("Incomes");
        }


        
        [HttpPost]
        public JsonResult SaveOutgo(CreateOutgoDto vm)
        {
            ServerResponse serverResponse = null;

            try
            {
                if (vm.Amount <= 0)
                {
                    return Json(ServerResponse.Error("Kwota musi być większa od zera"));
                }

                ICommonTransactionsServices transactionServices = new CommonTransactionsServices(db);
                transactionServices.AddOutgoes(vm.Name, vm.Amount, 1, vm.SettlementPeriodId);
                serverResponse = ServerResponse.Ok();
            }
            catch (Exception ex)
            {
                ////TODO: log ex to db
                serverResponse = ServerResponse.Error("Błąd wewnętrzny serwera. Proszę sprobować później");
            }



            return Json(serverResponse);
        }

        [HttpPost]
        public JsonResult SaveIncome(CreateOutgoDto vm)
        {
            ServerResponse serverResponse = null;

            try
            {
                if (vm.Amount <= 0)
                {
                    return Json(ServerResponse.Error("Kwota musi być większa od zera"));
                }

                ICommonTransactionsServices transactionServices = new CommonTransactionsServices(db);
                transactionServices.AddIncome(vm.Name, vm.Amount, 1, vm.SettlementPeriodId);
                serverResponse = ServerResponse.Ok();
            }
            catch (Exception ex)
            {
                ////TODO: log ex to db
                serverResponse = ServerResponse.Error("Błąd wewnętrzny serwera. Proszę sprobować później");
            }



            return Json(serverResponse);
        }

        [HttpGet]
        public JsonResult GetListOfOutcomes(int id)
        {
            var listOfTransactionFromdb = transactionServices.GetOutgoesCommonTransactions(1, id);
            List<TransactionDto> listTransactionDto = listOfTransactionFromdb.Select(x => new TransactionDto()
            {
                Name = x.Name,
                Amount = x.Amount
            }).ToList();

            return Json(listTransactionDto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListOfSettlementPeriod()
        {
            var listOfSettlementPeriodForAdd = settlementPeriodServices.GetListOfSettlementPeriods();

            monthListDto.Months = listOfSettlementPeriodForAdd.Select(x => new MonthsDto()
            {
                Id = x.Id,
                Date = x.Date.ToString("MMM yyyy")
            }).ToList();

            return Json(monthListDto.Months, JsonRequestBehavior.AllowGet);
        }
    }
}