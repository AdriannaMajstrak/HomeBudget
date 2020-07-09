using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudget.DataAccess;
using System.Runtime.CompilerServices;

namespace HomeBudget.Service.Model
{
    public class CommonTransactions : ICommonTransactions
    {

        public CommonTransactions(IEnumerable<Transaction> listOfAllTransactions)
        {
            ListOfAllTransactions = listOfAllTransactions;
            ListOfIncome = GetListOfIncome(listOfAllTransactions);
            ListOfOutgoes = GetListOfOutgoes(listOfAllTransactions);
            SumOfIncome = GetSumOfList(ListOfIncome);
            SumOfOutgoes = GetSumOfList(ListOfOutgoes);
            TransactionsDifferent = GetTransactionsDifferent(SumOfIncome, SumOfOutgoes);
        }

        public decimal TransactionsDifferent { get; set; }
        public decimal SumOfIncome { get; set; }
        public decimal SumOfOutgoes { get; set; }

        IEnumerable<Transaction> listOfAllTransactions;

        public IEnumerable<Transaction> ListOfAllTransactions
        {
            get
            {
                return listOfAllTransactions;
            }
            set
            {
                listOfAllTransactions = value;
               
                ListOfIncome = GetListOfIncome(listOfAllTransactions);
                ListOfOutgoes = GetListOfOutgoes(listOfAllTransactions);
                SumOfIncome = GetSumOfList(ListOfIncome);
                SumOfOutgoes = GetSumOfList(ListOfOutgoes);
                TransactionsDifferent = GetTransactionsDifferent(SumOfIncome, SumOfOutgoes);
            }
        }
        public IEnumerable<Transaction> ListOfIncome { get; set; }
        public IEnumerable<Transaction> ListOfOutgoes { get; set; }



        private decimal GetTransactionsDifferent(decimal sumOfIncome, decimal sumOfOutgoes)
        {
            ////sumOfOutgoes have a minus amount so I need just simply add income and outgoes
            return sumOfIncome + sumOfOutgoes;
        }

        private IEnumerable<Transaction> GetListOfIncome(IEnumerable<Transaction> listOfAllTransactions)
        {
            var listOfIn = listOfAllTransactions.Where(x => x.Amount > 0).Select(x=>x).ToList();

            return listOfIn;
        }

        private IEnumerable<Transaction> GetListOfOutgoes(IEnumerable<Transaction> listOfAllTransactions)
        {
            var listOfIn = listOfAllTransactions.Where(x => x.Amount <= 0).Select(x => x).ToList();

            return listOfIn;
        }

        private decimal GetSumOfList(IEnumerable<Transaction> listOfTransactions)
        {
            decimal sum = listOfTransactions.Sum(x => x.Amount);

            return sum;
        }
    }
}
