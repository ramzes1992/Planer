using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using Model;
using Model.Repositories;
using Microsoft.Practices.Prism.Commands;
using Planer.Views;

namespace Planer.ViewModels
{
    public class AccountHistoryViewModel : BaseViewModel
    {
        #region Properties

        #region Transactions
        public ObservableCollection<Transaction> Transactions { get; set; }
        #endregion

        #region Selected Transaction
        private Transaction _selectedTransaction;
        public Transaction SelectedTransaction
        {
            get
            {
                return _selectedTransaction;
            }

            set
            {
                if(_selectedTransaction != value)
                {
                    _selectedTransaction = value;
                    RaisePropertyChanged(() => SelectedTransaction);
                }
            }
        }
        #endregion

        #endregion

        #region Commands
        private void InitializeCommands()
        {
            AddTransactionCommand = new DelegateCommand(AddTransactionsExecute);
            RemoveTransactionCommand = new DelegateCommand(RemoveTransactionExecute);
        }

        #region Add Transaction
        public DelegateCommand AddTransactionCommand { get; set; }
        private void AddTransactionsExecute()
        {
            NewTransactionWindow view = new NewTransactionWindow(_currentAccount);
            var result = view.ShowDialog();
            if (result ?? false)
            {
                RefreshLists();
            }
        }
        #endregion

        #region Remove Transaction
        public DelegateCommand RemoveTransactionCommand { get; set; }
        private void RemoveTransactionExecute()
        {
            if(SelectedTransaction != null)
            {
                _transactionRepository.Remove(SelectedTransaction);
            }

            RefreshLists();
        }
        #endregion

        #endregion

        #region Members

        private Account _currentAccount;
        private AccountRepository _accountRepository = new AccountRepository();
        private TransactionRepository _transactionRepository = new TransactionRepository();

        #endregion

        #region Ctor
        public AccountHistoryViewModel(Account currentAccount)
        {
            this._currentAccount = currentAccount;

            if (_currentAccount != null)
            {
                RefreshLists();
            }

            InitializeCommands();
        }
        #endregion

        #region Private Methods

        private void RefreshLists()
        {
            Transactions = new ObservableCollection<Transaction>(_currentAccount.Transactions);
            RaisePropertyChanged(() => Transactions);
        }

        #endregion
    }
}
