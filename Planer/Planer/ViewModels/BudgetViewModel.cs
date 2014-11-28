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
using Planer.Models;
using System.Linq.Expressions;
using Planer.Helpers;
using Universal;

namespace Planer.ViewModels
{
    public class BudgetViewModel : BaseViewModel, IPage
    {
        #region Properties

        #region Accounts
        public ObservableCollection<AccountModel> Accounts { get; set; }
        #endregion

        #region Selected Account
        private AccountModel _selectedAccount;
        public AccountModel SelectedAccount
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
        public ObservableCollection<AccountModel> Moneyboxes { get; set; }
        #endregion

        #region Selected Moneybox
        private AccountModel _selectedMoneybox;
        public AccountModel SelectedMoneybox
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

            ShowAccountHistoryCommand = new DelegateCommand(ShowAccountHistoryExecute);
            ShowMoneyboxHistoryCommand = new DelegateCommand(ShowMoneyboxHistoryExecute);

            AddAccountTransactionCommand = new DelegateCommand(AddAccountTransactionExecute);
            AddMoneyboxTransactionCommand = new DelegateCommand(AddMoneyboxTransactionExecute);
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
                _accountRepository.Remove(_accountRepository.GetById(SelectedAccount.Id));
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
                _accountRepository.Remove(_accountRepository.GetById(SelectedMoneybox.Id));
                RefreshLists();
            }
        }
        #endregion

        #region Show History

        private void ShowHistory(int AccountId)
        {
            var viewModel = new AccountHistoryViewModel(_accountRepository.GetById(AccountId));
            viewModel.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName.Equals(ReflectionHelper.NameOf(() => viewModel.Transactions)))
                {
                    RefreshLists();
                }
            };
            AccountHistoryWindow view = new AccountHistoryWindow();
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        public DelegateCommand ShowAccountHistoryCommand { get; set; }
        private void ShowAccountHistoryExecute()
        {
            if (SelectedAccount != null)
            {
                ShowHistory(SelectedAccount.Id);
            }
        }

        public DelegateCommand ShowMoneyboxHistoryCommand { get; set; }
        private void ShowMoneyboxHistoryExecute()
        {
            if (SelectedMoneybox != null)
            {
                ShowHistory(SelectedMoneybox.Id);
            }
        }

        #endregion

        #region Add New Transaction

        public DelegateCommand AddAccountTransactionCommand { get; set; }

        private void AddAccountTransactionExecute()
        {
            if (SelectedAccount != null)
            {
                NewTransactionWindow view = new NewTransactionWindow(_accountRepository.GetById(SelectedAccount.Id));
                var result = view.ShowDialog();
                if (result ?? false)
                {
                    RefreshLists();
                }
            }
        }

        public DelegateCommand AddMoneyboxTransactionCommand { get; set; }

        private void AddMoneyboxTransactionExecute()
        {
            if (SelectedMoneybox != null)
            {
                NewTransactionWindow view = new NewTransactionWindow(_accountRepository.GetById(SelectedMoneybox.Id));
                var result = view.ShowDialog();
                if (result ?? false)
                {
                    RefreshLists();
                }
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
            this.Accounts = new ObservableCollection<AccountModel>(GetAccounts()
                .Where(a => a.Type.Equals(AccountType.Account)));
            RaisePropertyChanged(() => Accounts);

            this.Moneyboxes = new ObservableCollection<AccountModel>(GetAccounts()
                .Where(a => a.Type.Equals(AccountType.Moneybox)));
            RaisePropertyChanged(() => Moneyboxes);
        }

        private IEnumerable<AccountModel> GetAccounts()
        {
            if (_currentProject != null)
            {
                return _currentProject.Accounts.Select(a => new AccountModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = _accountRepository.GetAmount(a),
                    Type = (AccountType)a.Type
                });
            }

            return null;
        }
        #endregion

        public void Refresh()
        {
            RefreshLists();
        }
    }
}
