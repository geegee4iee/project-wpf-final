//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Unilever.DTO.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Debt
    {
        public int Year { get; set; }
        public int DistributorId { get; set; }
        public Nullable<decimal> Month1 { get; set; }
        public Nullable<decimal> Month2 { get; set; }
        public Nullable<decimal> Month3 { get; set; }
        public Nullable<decimal> Month4 { get; set; }
        public Nullable<decimal> Month5 { get; set; }
        public Nullable<decimal> Month6 { get; set; }
        public Nullable<decimal> Month7 { get; set; }
        public Nullable<decimal> Month8 { get; set; }
        public Nullable<decimal> Month9 { get; set; }
        public Nullable<decimal> Month10 { get; set; }
        public Nullable<decimal> Month11 { get; set; }
        public Nullable<decimal> Month12 { get; set; }
    
        public virtual Distributor Distributor { get; set; }
    }
}
