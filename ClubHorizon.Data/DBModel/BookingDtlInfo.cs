//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClubHorizon.Data.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookingDtlInfo
    {
        public int OrderDtlId { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int TimeSlotsId { get; set; }
        public Nullable<int> LocId { get; set; }
        public string TimeSlots { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
        public Nullable<System.DateTime> CancelledOn { get; set; }
        public Nullable<bool> IsRefunded { get; set; }
        public Nullable<System.DateTime> RefundedOn { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}
