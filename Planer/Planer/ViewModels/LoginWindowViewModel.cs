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
    public class LoginWindowViewModel : BaseViewModel
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
                if(value != _validationError)
                {
                    _validationError = value;
                    RaisePropertyChanged(() => ValidationError);
                }
            }
        }
        #endregion

        #endregion

        #region Commands

        public DelegateCommand<PasswordBox> LoginClick { get; private set; }

        public DelegateCommand RegisterClick { get; private set; }

        #endregion

        #region Ctor
        public LoginWindowViewModel(Window view)
        {
            LoginClick = new DelegateCommand<PasswordBox>(LoginExecute);

            RegisterClick = new DelegateCommand(RegisterExecute);

            _userRepository.Login(string.Empty, string.Empty);// quick connection with database

            _view = view;
        }
        #endregion

        private void LoginExecute(PasswordBox passwordBox)
        {
            var password = passwordBox.Password;

            if (_userRepository.Login(UserName, password))
            {
                _view.DialogResult = true;
                _view.Close();
            }
            else
            {
                ValidationError = "User Name or Password is incorrect.";
            }
        }

        private void RegisterExecute()
        {
            Window registerView = new RegisterWindow();
            RegisterWindowViewModel registerViewModel = new RegisterWindowViewModel(registerView, _userRepository);

            registerView.Owner = _view;
            registerView.DataContext = registerViewModel;

            var result = registerView.ShowDialog();
            //TODO: zaloguj jeśli zarejetrowano
        }

        #region Members

        private static readonly UserRepository _userRepository = new UserRepository();

        private Window _view;

        #endregion
    }
}
