using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Microsoft.Practices.Prism.Commands;

namespace Planer.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
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
                if(value != _userName)
                {
                    _userName = value;
                    RaisePropertyChanged(() => UserName);
                }
            }
        }
        #endregion

        #endregion

        #region Members

       

        #endregion

        #region Commands



        #endregion

        #region Ctor
        public MainWindowViewModel(string currentUserName)
        {
            UserName = currentUserName;
        }

        #endregion

    }
}
