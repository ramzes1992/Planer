using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal;

namespace Model.Repositories
{
    public class AccountRepository : BaseRepository
    {
        public IQueryable<Account> GetAll()
        {
            return Entities.Accounts.AsQueryable();
        }

        public Account GetById(int Id)
        {
            return Entities.Accounts.SingleOrDefault(a => a.Id.Equals(Id));
        }

        public void Add(Account account)
        {
            if (account != null)
            {
                Entities.Accounts.Add(account);
                Entities.SaveChanges();

                if (account.Node != null)
                {
                    RecalculateProgresses(account.Node);
                }
            }
        }

        public decimal GetAmount(Account account)
        {
            if (account != null)
            {
                return account.Transactions.Sum(t => t.Amount) + account.Startup;
            }

            return 0;
        }

        public decimal GetAmount(int accountId)
        {
            return GetAmount(GetById(accountId));
        }

        public void Edit(Account account)
        {
            Entities.SaveChanges();

            if (account.Node != null)
            {
                RecalculateProgresses(account.Node);
            }
        }

        public void Remove(Account account)
        {
            if (account != null)
            {
                Entities.Transactions.RemoveRange(account.Transactions);
                Entities.Accounts.Remove(account);
                Entities.SaveChanges();

                if (account.Node != null)
                {
                    RecalculateProgresses(account.Node);
                }
            } 
        }

    }
}
