using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class NodeRepository : BaseRepository
    {
        public IQueryable<Node> GetAll()
        {
            return Entities.Nodes.AsQueryable();
        }

        public void Add(string text, Project project)
        {
            if(project != null && !string.IsNullOrWhiteSpace(text))
            {
                Node node = new Node()
                {
                    Name = text,
                    Project = project
                };

                Entities.Nodes.Add(node);
                Entities.SaveChanges();
            }
        }
    }
}
