﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model.Enums;

namespace Model.Repositories
{
    public class BaseRepository
    {
        protected static readonly PlanerDatabaseEntities Entities = new PlanerDatabaseEntities();

        protected static void RecalculateProgresses(Node child)
        {
            var current = child;//for tests... TODO: 
            while (current != null)
            {
                current.Progress = GetProgressFlat(current);

                current = current.Parent;
            }

            Entities.SaveChanges();
        }

        private static int GetProgressFlat(Node root)
        {
            if (root.Children.Any() || root.Tasks.Any())
            {
                int progress = 0;

                int itemsCount = root.Children.Count + root.Tasks.Count; // can't be equals 0;

                foreach (var node in root.Children)
                {
                    progress += node.Progress;
                }

                foreach (var task in root.Tasks)
                {
                    if (task.State == (int)TaskState.Done)
                    {
                        progress += 100;
                    }
                }

                return (int)(progress / itemsCount);
            }

            return 0;
        }
    }
}
