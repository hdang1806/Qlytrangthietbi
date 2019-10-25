//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyTaiSan_UserManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DeviceOfProject
    {
        public int Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public Nullable<int> DeviceId { get; set; }
        public Nullable<System.DateTime> DateOfDelivery { get; set; }
        public Nullable<System.DateTime> DateOfReturn { get; set; }
        public Nullable<int> Status { get; set; }
        public string Notes { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual Device Device { get; set; }
        public virtual ProjectDKC ProjectDKC { get; set; }
    }
}
