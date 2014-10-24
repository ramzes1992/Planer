using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class BaseRepository
    {
        protected static readonly PlanerDatabaseEntities Entities = new PlanerDatabaseEntities();
    }
}
