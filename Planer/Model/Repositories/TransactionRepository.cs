using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class TransactionRepository : BaseRepository
    {
        public IQueryable GetAll()
        {
            return Entities.Transactions;
        }

        public Transaction GetById(int id)
        {
            return Entities.Transactions.SingleOrDefault(t => t.Id.Equals(id));
        }

        public void Add(Transaction transaction)
        {
            if (transaction != null)
            {
                Entities.Transactions.Add(transaction);
                Entities.SaveChanges();

                if(transaction.Account != null
                    && transaction.Account.Node != null)
                {
                    RecalculateProgresses(transaction.Account.Node);
                }

            }
        }

        public void Remove(Transaction transaction)
        {
            if(transaction != null)
            {
                Entities.Transactions.Remove(transaction);
                Entities.SaveChanges();

                if (transaction.Account != null
                    && transaction.Account.Node != null)
                {
                    RecalculateProgresses(transaction.Account.Node);
                }
            }
        }
    }
}
