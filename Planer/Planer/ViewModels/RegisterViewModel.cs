using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model.Repositories;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace Planer.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Properties

        #region User Name
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
                    RegisterClick.RaiseCanExecuteChanged();
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
                    RegisterClick.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Confirm Password
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                if (value != _confirmPassword)
                {
                    _confirmPassword = value;
                    RaisePropertyChanged(() => ConfirmPassword);
                    RegisterClick.RaiseCanExecuteChanged();
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

        private UserRepository _userRepository;

        private Window _view;

        #endregion

        #region Commands

        #region Register Click
        public DelegateCommand RegisterClick { get; private set; }

        private bool RegisterCanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrWhiteSpace(Password)
                && Password.Equals(ConfirmPassword);
        }

        private void RegisterExecute()
        {
            var hash = Helpers.HashHelper.GetHashAsGuid(Password).ToString();

            if (!_userRepository.Exists(UserName))
            {
                _userRepository.Add(UserName, hash);
                _view.DialogResult = true;
                _view.Close();
            }
            else
            {
                ValidationError = "User Name already in use.";
            }
        }
        #endregion

        #endregion

        #region Ctor

        public RegisterViewModel(Window view, UserRepository userRepo)
        {
            _userRepository = userRepo;
            _view = view;

            RegisterClick = new DelegateCommand(RegisterExecute, RegisterCanExecute);
        }

        #endregion

        #region private Methods

        

        #endregion

    }
}
