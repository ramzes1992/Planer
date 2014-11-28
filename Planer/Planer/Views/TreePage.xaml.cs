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

using Planer.ViewModels;
using Model;
using Planer.Helpers;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for TreePage.xaml
    /// </summary>
    public partial class TreePage : Page
    {        
        public TreePage()
        {
            InitializeComponent();
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = ViewHelper.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void ContextMenu_AddNewNodeItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if(viewModel != null)
            {
                viewModel.AddNewNodeCommand.Execute();
            }
        }

        private void ContextMenu_RemoveNodeItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.RemoveNodeCommand.Execute();
            }
        }

        private void ContextMenu_EditNodeItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.EditNodeCommand.Execute();
            }
        }

        private void ContextMenu_EditTaskItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.EditTaskCommand.Execute();
            }
        }

        private void ContextMenu_RemoveTaskItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.RemoveTaskCommand.Execute();
            }
        }

        private void ContextMenu_AssignMoneybox_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.AssignMoneyboxCommand.Execute();
            }
        }

        private void ContextMenu_UnassignMoneyboxItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TreeViewModel)this.DataContext;

            if (viewModel != null)
            {
                viewModel.UnassignMoneyboxCommand.Execute();
            }
        }
    }
}
