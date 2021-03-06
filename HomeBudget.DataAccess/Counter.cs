//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeBudget.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Counter
    {
        public int Id { get; set; }
        public int SettlementPeriodId { get; set; }
        public string Name { get; set; }
        public decimal AmountCounter { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<int> TransactionOutgoesId { get; set; }
        public bool Equalized { get; set; }
        public Nullable<int> TransactionIncomeId { get; set; }
        public Nullable<int> DestinationAccountGroupId { get; set; }
        public Nullable<decimal> ForecastIncrease { get; set; }
        public bool Equalizable { get; set; }
    
        public virtual AccountGroup AccountGroup { get; set; }
        public virtual SettlementPeriod SettlementPeriod { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual Transaction Transaction1 { get; set; }
    }
}
