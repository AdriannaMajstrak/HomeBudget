using HomeBudget.Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.Model
{
    public class Counter : INotifyPropertyChanged
    {
        private string name;
        private decimal amountCounter;
        private bool equalized;
        private bool equalizable;
        private decimal increase;
        private decimal surcharge;
        private int id;

        public event EventHandler<decimal> AmountChanged;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                Notify();
            }
        }

        public decimal AmountCounter
        {
            get
            {
                return amountCounter;
            }
            set
            {
                amountCounter = value;
                AmountChanged?.Invoke(this, value);
                Notify();
            }
        }

        public bool Equalized
        {
            get
            {
                return equalized;
            }
            set
            {
                equalized = value;
                Notify();
            }
        }

        public bool Equalizable
        {
            get
            {
                return equalizable;
            }
            set
            {
                equalizable = value;
                Notify();
            }
        }

        public decimal Increase
        {
            get
            {
                return increase;
            }
            set
            {
                increase = value;
                Notify();
            }
        }

        public decimal Surcharge
        {
            get
            {
                return surcharge;
            }
            set
            {
                surcharge = value;
                Notify();
            }
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

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
