using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Enums;

namespace Model.Repositories
{
    public class TaskRepository : BaseRepository
    {
        public IQueryable<Task> GetAll()
        {
            return Entities.Tasks.AsQueryable();
        }

        public Task GetById(int Id)
        {
            return Entities.Tasks.SingleOrDefault(u => u.Id.Equals(Id));
        }

        public void Add(Task task)
        {
            Entities.Tasks.Add(task);
            Entities.SaveChanges();
        }

        public void Edit(Task task)
        {
            Entities.SaveChanges();
        }

        public void Remove(Task task)
        {
            Entities.Tasks.Remove(task);
            Entities.SaveChanges();
        }

        public void ChangePriority(Task task, EisenhowerPriority? newPriority = null)
        { 
            if(task != null)
            {
                task.Priority = (int?)newPriority;
                Entities.SaveChanges();
            }
        }
    }
}
