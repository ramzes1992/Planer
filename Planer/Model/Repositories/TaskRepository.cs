using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
