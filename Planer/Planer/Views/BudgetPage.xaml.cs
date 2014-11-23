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
    }
}
