using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Prism.Commands;
using System.Security;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using Model.Repositories;
using Planer.Views;

namespace Planer.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties

        #region UserName
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    RaisePropertyChanged(() => UserName);
                    LoginClick.RaiseCanExecuteChanged();
                    ValidationError = string.Empty;
                }
            }
        }
        #endregion

        #region Password

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged(() => Password);
                    LoginClick.RaiseCanExecuteChanged();
                    ValidationError = string.Empty;
                }
            }
        }

        #endregion

        #region Validation Error Message
        private string _validationError;
        public string ValidationError
        {
            get
            {
                return _validationError;
            }
            set
            {
                if (value != _validationError)
                {
                    _validationError = value;
                    RaisePropertyChanged(() => ValidationError);
                }
            }
        }
        #endregion

        #endregion

        #region Members

        private static readonly UserRepository _userRepository = new UserRepository();

        private Window _view;

        #endregion

        #region Commands

        #region Login Click
        public DelegateCommand LoginClick { get; private set; }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrEmpty(Password);
        }

        private void LoginExecute()
        {
            var hash = Helpers.HashHelper.GetHashAsGuid(Password).ToString();
            if (_userRepository.Login(UserName, hash))
            {
                _view.DialogResult = true;
                _view.Close();
            }
            else
            {
                ValidationError = "User Name or Password is incorrect.";
            }
        }

        #endregion

        #region Register Click
        public DelegateCommand RegisterClick { get; private set; }

        private void RegisterExecute()
        {
            Window registerView = new RegisterWindow();
            RegisterViewModel registerViewModel = new RegisterViewModel(registerView, _userRepository);

            registerView.Owner = _view;
            registerView.DataContext = registerViewModel;

            var result = registerView.ShowDialog();

            if (result ?? false)
            {
                ValidationError = string.Empty;
                UserName = registerViewModel.UserName;
                Password = string.Empty;
            }
        }

        #endregion

        #endregion

        #region Ctor

        public LoginViewModel(Window view)
        {
            LoginClick = new DelegateCommand(LoginExecute, LoginCanExecute);

            RegisterClick = new DelegateCommand(RegisterExecute);

            _userRepository.Login(string.Empty, string.Empty);// to speedup connection with database

            _view = view;
        }

        #endregion

        #region private Methods



        #endregion

    }
}
