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
    
    public partial class Task
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int State { get; set; }
        public Nullable<int> NodeId { get; set; }
        public int ProjectId { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual Node Node { get; set; }
    }
}
