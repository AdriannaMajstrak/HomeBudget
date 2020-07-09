using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.Model
{
    public class CyclePayment : INotifyPropertyChanged
    {
        private int id;
        private int settlementPeriodId;
        private string name;
        private decimal amount;
        private int? transactionOutgoesId;
        private int? transactionIncomeId;
        private int? destinationAccountGroupId;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                Notify();
            }
        }

        public int SettlementPeriodId { get => settlementPeriodId; set { settlementPeriodId = value; Notify(); } }

        public string Name { get => name; set { name = value; Notify();} }

        public decimal Amount { get => amount; set { amount = value; Notify(); } }

        public int? TransactionOutgoesId { get => transactionOutgoesId; set { transactionOutgoesId = value; Notify(); } }

        public int? TransactionIncomeId { get => transactionIncomeId; set { transactionIncomeId = value; Notify(); } }

        public int? DestinationAccountGroupId { get => destinationAccountGroupId; set { destinationAccountGroupId = value; Notify(); } }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
