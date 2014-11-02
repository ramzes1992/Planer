using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class ProjectRepository : BaseRepository
    {
        public IQueryable<Project> GetAll()
        {
            return Entities.Projects.AsQueryable();
        }

        public Project GetById(int Id)
        {
            return Entities.Projects.SingleOrDefault(u => u.Id.Equals(Id));
        }

        public void Add(Project newProject)
        {
            if (newProject != null)
            {
                Entities.Projects.Add(newProject);
                Entities.SaveChanges();
            }
        }

        public void Add(string name, User owner)
        {
            Project newProject = new Project();
            newProject.Name = name;
            newProject.Owner = owner;

            Entities.Projects.Add(newProject);
            Entities.SaveChanges();
        }

        public void Add(string name, string ownerName)
        {
            Project newProject = new Project();
            newProject.Name = name;
            newProject.Owner = Entities.Users.FirstOrDefault(u => u.Name.Equals(ownerName));

            if (newProject != null)
            {
                Entities.Projects.Add(newProject);
                Entities.SaveChanges();
            }
        }

    }
}
