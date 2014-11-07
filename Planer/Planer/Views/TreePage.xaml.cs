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
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
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
    }
}
