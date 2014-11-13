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

            if (task.Node != null)
            {
                RecalculateProgresses(task.Node);
            }
        }

        public void Edit(Task task)
        {
            Entities.SaveChanges();

            if (task.Node != null)
            {
                RecalculateProgresses(task.Node);
            }
        }

        public void Remove(Task task)
        {
            var tmpNode = task.Node;

            Entities.Tasks.Remove(task);
            Entities.SaveChanges();

            if (tmpNode != null)
            {
                RecalculateProgresses(tmpNode);
            }
        }

        public void ChangePriority(Task task, EisenhowerPriority? newPriority = null)
        {
            if (task != null)
            {
                task.Priority = (int?)newPriority;
                Entities.SaveChanges();
            }
        }
    }
}
