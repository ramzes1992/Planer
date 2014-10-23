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
    public class RegisterWindowViewModel : BaseViewModel
    {
        #region Properties

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if(value != _userName)
                {
                    _userName = value;
                    RaisePropertyChanged(() => UserName);
                    RegisterClick.RaiseCanExecuteChanged();
                }
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if(_password != value)
                {
                    _password = value;
                    RaisePropertyChanged(() => Password);
                    RegisterClick.RaiseCanExecuteChanged();
                }
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                if(value != _confirmPassword)
                {
                    _confirmPassword = value;
                    RaisePropertyChanged(() => ConfirmPassword);
                    RegisterClick.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region Commands

        public DelegateCommand RegisterClick { get; private set; }

        #endregion

        #region Members

        private UserRepository _userRepository;

        private Window _view;

        #endregion

        #region Ctor

        public RegisterWindowViewModel(Window view,UserRepository userRepo)
        {
            _userRepository = userRepo;
            _view = view;

            RegisterClick = new DelegateCommand(RegisterExecute, RegisterCanExecute);
        }

        private bool RegisterCanExecute()
        {
            return string.IsNullOrWhiteSpace(UserName)
                && !string.IsNullOrWhiteSpace(Password)
                && Password.Equals(ConfirmPassword);
        }

        private void RegisterExecute()
        {
            //TODO: LOGIKA!!

            _view.Close();
        }


        #endregion

    }
}
