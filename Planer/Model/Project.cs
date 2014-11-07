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
    
    public partial class Project
    {
        public Project()
        {
            this.Collaborators = new HashSet<Collaborator>();
            this.Tasks = new HashSet<Task>();
            this.Nodes = new HashSet<Node>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
    
        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Node> Nodes { get; set; }
    }
}
