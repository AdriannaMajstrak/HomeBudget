using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Client.Model
{
    using Service;
    using System;

    public sealed class Settings
    {
        private static volatile Settings instance;
        private static object syncRoot = new Object();

        private Settings()
        {
            ActualSettlementPeriod = 
                new ActualSettlementPeriod(actualSettlementPeriodId, spServices.GetSettlementPeriodDateById(actualSettlementPeriodId));
        }

        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Settings();
                    }
                }

                return instance;
            }
        }

        SettlementPeriodServices spServices = new SettlementPeriodServices(new DataAccess.BudgetEntities());
        int actualSettlementPeriodId = Properties.Settings.Default.ActualSettlementPeriodId;

        internal ActualSettlementPeriod ActualSettlementPeriod { get; set; }
    }
}
