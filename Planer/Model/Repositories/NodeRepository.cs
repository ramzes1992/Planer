using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Repositories
{
    public class NodeRepository : BaseRepository
    {
        public IQueryable<Node> GetAll()
        {
            return Entities.Nodes.AsQueryable();
        }

        public Node GetById(int Id)
        {
            return Entities.Nodes.SingleOrDefault(n => n.Id.Equals(Id));
        }

        public Node Add(string text, Project project)
        {
            if (project != null && !string.IsNullOrWhiteSpace(text))
            {
                Node node = new Node()
                {
                    Name = text,
                    Project = project
                };

                Entities.Nodes.Add(node);
                Entities.SaveChanges();

                RecalculateProgresses(node);

                return node;
            }

            return null;
        }

        public Node Add(string text, Node parent)
        {
            if (parent != null && !string.IsNullOrWhiteSpace(text))
            {
                Node node = new Node()
                {
                    Name = text,
                    Project = parent.Project,
                    Parent = parent
                };

                Entities.Nodes.Add(node);
                Entities.SaveChanges();

                RecalculateProgresses(node);

                return node;
            }

            return null;
        }

        public void Edit(Node node)
        {
            var entry = Entities.Entry(node);
            if (entry.CurrentValues.PropertyNames.Any(p => entry.Property(p).IsModified
                                                           && p.Equals("Progress")))
            {
                RecalculateProgresses(node);
            }

            Entities.SaveChanges();
        }

        public void Remove(Node node)
        {
            if (node.Children.Any())
            {
                foreach (var child in node.Children.ToList())
                {
                    Remove(child);
                }
            }

            var tmp = node.Parent;
            Entities.Nodes.Remove(node);
            Entities.SaveChanges();

            if (tmp != null)
            {
                RecalculateProgresses(tmp);
            }
        }
    }
}
