using HomeBudget.Client.Model;
using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;

namespace HomeBudget.Client.ViewModel
{
    public class CheckPointViewModel : ViewModelBase, IPageViewModel
    {

        private List<SettlementPeriod> listOfSettlementPeriod;
        private SettlementPeriod selectedSettlementPeriod;
        private List<CheckPoint> listOfCheckPoints;
        private CheckPoint selectedCheckPoint;

        
        /// TODO:2. Zamiast Pojedynczych pol tablica, jedna na kazda grupe kont
        
       


        public CheckPointViewModel(BudgetEntities budgetEntities)
        {

        }

        public List<SettlementPeriod> ListOfSettlementPeriod
        {
            get
            {
                return listOfSettlementPeriod;
            }
            set
            {
                listOfSettlementPeriod = value;
                Notify();
            }
        }

        public SettlementPeriod SelectedSettlementPeriod
        {
            get
            {
                return selectedSettlementPeriod;
            }
            set
            {
                selectedSettlementPeriod = value;
                Notify();
            }
        }

        public List<CheckPoint> ListOfCheckPoints
        {
            get
            {
                return listOfCheckPoints;
            }
            set
            {
                listOfCheckPoints = value;
                Notify();
            }
        }

        public CheckPoint SelectedCheckPoint
        {
            get
            {
                return selectedCheckPoint;
            }
            set
            {
                selectedCheckPoint = value;
                Notify();
            }
        }

        public void CreateNewCheckPoint()
        { }

        public void ShowSelectedCheckPoint() { }

        public event EventHandler<DataToRefresh> RefreshData;
    }
}
