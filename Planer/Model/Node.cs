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
    
    public partial class Node
    {
        public Node()
        {
            this.Children = new HashSet<Node>();
            this.Accounts = new HashSet<Account>();
            this.Tasks = new HashSet<Task>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int ProjectId { get; set; }
        public int Progress { get; set; }
    
        public virtual ICollection<Node> Children { get; set; }
        public virtual Node Parent { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
