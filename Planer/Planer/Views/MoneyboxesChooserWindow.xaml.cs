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

using Model.Repositories;
using Planer.Models;
using Model;
using Model.Enums;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for MoneyboxesChooserWindow.xaml
    /// </summary>
    public partial class MoneyboxesChooserWindow : Window
    {
        private AccountRepository _accountRepo = new AccountRepository();
        private Project _currentProject;
        private Node _currentNode;

        public MoneyboxesChooserWindow(Project currentProject, Node currentNode)
        {
            this._currentProject = currentProject;
            this._currentNode = currentNode;

            if (_currentProject == null || _currentNode == null)
            {
                DialogResult = false;
                Close();
            }

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            v_ListBox_Moneyboxes.ItemsSource = GetUnsignedAccounts().Where(a => a.Type.Equals(AccountType.Moneybox));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedMoneyBox = _accountRepo.GetById((v_ListBox_Moneyboxes.SelectedItem as AccountModel).Id);
            if (selectedMoneyBox != null
                && selectedMoneyBox.Type.Equals((int)AccountType.Moneybox))
            {
                selectedMoneyBox.Node = _currentNode;
                _accountRepo.Edit(selectedMoneyBox);

                DialogResult = true;
                Close();
            }
        }

        private void v_ListBox_Moneyboxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (v_ListBox_Moneyboxes.SelectedItem != null)
            {
                v_Button_Assign.IsEnabled = true;
            }
            else
            {
                v_Button_Assign.IsEnabled = false;
            }
        }

        private IEnumerable<AccountModel> GetUnsignedAccounts()
        {
            if (_currentProject != null)
            {
                return _currentProject.Accounts.Where(a => !a.NodeId.HasValue).Select(a => new AccountModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Amount = _accountRepo.GetAmount(a),
                    Type = (AccountType)a.Type
                });
            }

            return null;
        }
    }
}
