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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Planer.Helpers;
using Planer.ViewModels;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for BudgetPage.xaml
    /// </summary>
    public partial class BudgetPage : Page
    {
        public BudgetPage()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = ViewHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject);

            if (listBoxItem != null)
            {
                listBoxItem.Focus();
                listBoxItem.IsSelected = true;
                e.Handled = true;
            }
        }

        private void ListBoxItemContextMenu_RemoveAccountClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.RemoveAccountCommand.Execute();
            }
        }

        private void ListBoxItemContextMenu_RemoveMoneyboxClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.RemoveMoneyboxCommand.Execute();
            }
        }

        private void ListBoxItemContextMenu_ShowAccountHistoryClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.ShowAccountHistoryCommand.Execute();
            }
        }

        private void ListBoxItemContextMenu_ShowMoneyboxHistoryClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.ShowMoneyboxHistoryCommand.Execute();
            }
        }

        private void ListBoxItemContextMenu_AddTransactionToAccountClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.AddAccountTransactionCommand.Execute();
            }
        }

        private void ListBoxItemContextMenu_AddTransactionToMoneyboxClick(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.AddMoneyboxTransactionCommand.Execute();
            }
        }

        private void ListBoxItemDoubleClick_Accounts(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.AddAccountTransactionCommand.Execute();
            }
        }

        private void ListBoxItemDoubleClick_Moneyboxes(object sender, RoutedEventArgs e)
        {
            BudgetViewModel viewModel = this.DataContext as BudgetViewModel;
            if (viewModel != null)
            {
                viewModel.AddMoneyboxTransactionCommand.Execute();
            }
        }
    }
}
