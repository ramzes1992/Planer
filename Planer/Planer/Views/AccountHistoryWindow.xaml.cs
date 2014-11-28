using Planer.ViewModels;
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

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for AccountHistoryWindow.xaml
    /// </summary>
    public partial class AccountHistoryWindow : Window
    {
        public AccountHistoryWindow()
        {
            InitializeComponent();
        }

        private void ItemContextMenu_RemoveClick(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as AccountHistoryViewModel;
            if (viewModel != null)
            {
                viewModel.RemoveTransactionCommand.Execute();
            }
        }
    }
}
