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
                }
            }
        }
        #endregion

        #endregion

        #region Commands

        public DelegateCommand<PasswordBox> LoginClick { get; private set; }

        #endregion

        #region Ctor
        public LoginWindowViewModel(Window view)
        {
            LoginClick = new DelegateCommand<PasswordBox>(LoginExecute);

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
                //TODO: MessageBox Invalid Login or Password
            }

        }

        #region Members

        private static readonly UserRepository _userRepository = new UserRepository();

        private Window _view;

        #endregion
    }
}
