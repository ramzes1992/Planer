//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int Type { get; set; }
        public Nullable<int> NodeId { get; set; }
        public decimal Startup { get; set; }
    
        public virtual Node Node { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
