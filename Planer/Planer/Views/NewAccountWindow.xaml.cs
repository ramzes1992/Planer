using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Model;
using Model.Repositories;
using Model.Enums;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        Project _currentProject;
        AccountRepository _accountRepository = new AccountRepository();
        AccountType _currentType;

        public NewAccountWindow(Project currentProject, AccountType type)
        {
            InitializeComponent();

            this._currentProject = currentProject;
            
            if (_currentProject == null)
            {
                DialogResult = false;
                Close();
            }

            this._currentType = type;

            switch(_currentType)
            {
                case AccountType.Account:
                    Title = "New Account";
                    break;
                case AccountType.Moneybox:
                    Title = "New MoneyBox";
                    break;
                default:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(v_TextBox_AccountName.Text))
            {
                if (_currentProject != null)
                {
                    _accountRepository.Add(new Account()
                    {
                        Name = v_TextBox_AccountName.Text,
                        Project = _currentProject,
                        Type = (int)_currentType
                    });
                    this.DialogResult = true;
                    Close();
                }
            }
        }

        private void v_TextBox_AccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            v_Button_Create.IsEnabled = !string.IsNullOrWhiteSpace(v_TextBox_AccountName.Text);
        }
    }
}
