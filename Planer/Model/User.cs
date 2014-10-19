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
    
    public partial class User
    {
        public User()
        {
            this.Collaborators = new HashSet<Collaborator>();
            this.Permissions = new HashSet<Permission>();
            this.Projects = new HashSet<Project>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    
        public virtual ICollection<Collaborator> Collaborators { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}