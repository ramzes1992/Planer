using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Add(Account acconut)
        {
            if (acconut != null)
            {
                Entities.Accounts.Add(acconut);
                Entities.SaveChanges();
            }
        }

        public decimal GetAmount(Account account)
        {
            if (account != null)
            {
                return account.Transactions.Sum(t => t.Amount);
            }

            return 0;
        }

        public decimal GetAmount(int accountId)
        {
            return GetAmount(GetById(accountId));
        }

        public void Remove(Account account)
        {
            if (account != null)
            {
                Entities.Accounts.Remove(account);
                Entities.SaveChanges();
            } 
        }

    }
}
