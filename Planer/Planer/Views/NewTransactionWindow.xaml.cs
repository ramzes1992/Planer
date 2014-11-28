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
using Xceed.Wpf.Toolkit;
using Model.Enums;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for AddTransactionWindow.xaml
    /// </summary>
    public partial class NewTransactionWindow : Window
    {
        //TODO: implemetation

        private Account _currentAccount;
        private AccountRepository _accountRepository = new AccountRepository();
        private TransactionRepository _transactionRepository = new TransactionRepository();

        public NewTransactionWindow(Account currentAccount)
        {
            this._currentAccount = currentAccount;

            if (_currentAccount == null)
            {
                DialogResult = false;
                Close();
            }

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAccount != null)
            {
                _transactionRepository.Add(new Transaction()
                {
                    Text = v_TextBox.Text,
                    Amount = v_RadioButton_isIncome.IsChecked.Value ? (v_DecimalUpDown.Value ?? 0) : -(v_DecimalUpDown.Value ?? 0),
                    Account = _currentAccount
                });

                DialogResult = true;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_currentAccount != null 
                && _currentAccount.Type.Equals((int)AccountType.Moneybox))
            {
                v_RadioButton_isIncome.IsChecked = true;

                v_RadioButton_isIncome.Visibility = System.Windows.Visibility.Hidden;
                v_RadioButton_isExpanse.Visibility = System.Windows.Visibility.Hidden;
                v_Label_TypeOfTransaction.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
