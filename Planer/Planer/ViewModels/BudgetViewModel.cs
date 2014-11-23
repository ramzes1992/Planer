using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Collections.ObjectModel;
using Model.Repositories;
using Microsoft.Practices.Prism.Commands;
using Planer.Views;
using Model.Enums;

namespace Planer.ViewModels
{
    public class BudgetViewModel : BaseViewModel
    {
        #region Properties

        #region Accounts
        public ObservableCollection<Account> Accounts { get; set; }
        #endregion

        #region Selected Account
        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get
            {
                return _selectedAccount;
            }

            set
            {
                if (_selectedAccount != value)
                {
                    _selectedAccount = value;
                    RaisePropertyChanged(() => SelectedAccount);
                }
            }
        }
        #endregion

        #region Moneyboxes
        public ObservableCollection<Account> Moneyboxes { get; set; }
        #endregion

        #region Selected Moneybox
        private Account _selectedMoneybox;
        public Account SelectedMoneybox
        {
            get
            {
                return _selectedMoneybox;
            }

            set
            {
                if (_selectedMoneybox != value)
                {
                    _selectedMoneybox = value;
                    RaisePropertyChanged(() => SelectedMoneybox);
                }
            }
        }
        #endregion

        #endregion

        #region Commands

        private void InitializeCommands()
        {
            AddAccountCommad = new DelegateCommand(AddAccountExecute);
            RemoveAccountCommand = new DelegateCommand(RemoveAccountExecute);

            AddMoneyboxCommad = new DelegateCommand(AddMoneyboxExecute);
            RemoveMoneyboxCommand = new DelegateCommand(RemoveMoneyboxExecute);
        }

        #region Add Account
        public DelegateCommand AddAccountCommad { get; set; }
        private void AddAccountExecute()
        {
            NewAccountWindow view = new NewAccountWindow(_currentProject, AccountType.Account);

            var result = view.ShowDialog();
            if (result ?? false)
            {
                RefreshLists();
            }
        }
        #endregion

        #region Remove Command
        public DelegateCommand RemoveAccountCommand { get; set; }
        private void RemoveAccountExecute()
        {
            if (SelectedAccount != null)
            {
                _accountRepository.Remove(SelectedAccount);
                RefreshLists();
            }
        }
        #endregion

        #region Add Moneybox
        public DelegateCommand AddMoneyboxCommad { get; set; }
        private void AddMoneyboxExecute()
        {
            NewAccountWindow view = new NewAccountWindow(_currentProject, AccountType.Moneybox);

            var result = view.ShowDialog();
            if (result ?? false)
            {
                RefreshLists();
            }
        }
        #endregion

        #region Remove Moneybox
        public DelegateCommand RemoveMoneyboxCommand { get; set; }
        private void RemoveMoneyboxExecute()
        {
            if (SelectedMoneybox != null)
            {
                _accountRepository.Remove(SelectedMoneybox);
                RefreshLists();
            }
        }
        #endregion

        #endregion

        #region Members
        AccountRepository _accountRepository = new AccountRepository();
        Project _currentProject;
        #endregion

        #region Ctor

        public BudgetViewModel(Project currentProject)
        {
            if (currentProject != null)
            {
                this._currentProject = currentProject;
                RefreshLists();
            }

            InitializeCommands();
        }

        #endregion

        #region Provate Methods
        private void RefreshLists()
        {
            this.Accounts = new ObservableCollection<Account>(_currentProject.Accounts
                .Where(a => a.Type.Equals((int)AccountType.Account)));
            RaisePropertyChanged(() => Accounts);

            this.Moneyboxes = new ObservableCollection<Account>(_currentProject.Accounts
                .Where(a => a.Type.Equals((int)AccountType.Moneybox)));
            RaisePropertyChanged(() => Moneyboxes);
        }
        #endregion
    }
}
