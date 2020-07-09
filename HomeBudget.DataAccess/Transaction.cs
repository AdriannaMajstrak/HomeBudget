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
    
    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            this.Counters = new HashSet<Counter>();
            this.Counters1 = new HashSet<Counter>();
            this.CyclePayments = new HashSet<CyclePayment>();
            this.CyclePayments1 = new HashSet<CyclePayment>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int AccountGroupId { get; set; }
        public int SettlementPeriodId { get; set; }
    
        public virtual AccountGroup AccountGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Counter> Counters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Counter> Counters1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CyclePayment> CyclePayments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CyclePayment> CyclePayments1 { get; set; }
        public virtual SettlementPeriod SettlementPeriod { get; set; }
    }
}
