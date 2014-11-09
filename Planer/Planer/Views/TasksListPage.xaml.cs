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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for TasksListPage.xaml
    /// </summary>
    public partial class TasksListPage : Page
    {
        public TasksListPage()
        {
            InitializeComponent();
        }

        private void ListBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem listBoxItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (listBoxItem != null)
            {
                listBoxItem.Focus();
                listBoxItem.IsSelected = true;
                e.Handled = true;
            }
        }

        static ListBoxItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is ListBoxItem))
                source = VisualTreeHelper.GetParent(source);

            return source as ListBoxItem;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem)sender;

            if (menuItem != null)
            {
                TasksListViewModel viewModel = this.DataContext as TasksListViewModel;

                if (viewModel != null)
                {
                    switch (menuItem.Tag.ToString())
                    {
                        case "ImportantAndUrgent":
                            viewModel.MoveToImportantAndUrgentTasksCommand.Execute();
                            break;
                        case "ImportantAndNotUrgent":
                            viewModel.MoveToImportantAndNotUrgentTasksCommand.Execute();
                            break;
                        case "NotImportantAndUrgent":
                            viewModel.MoveToNotImportantAndUrgentTasksCommand.Execute();
                            break;
                        case "NotImportantAndNotUrgent":
                            viewModel.MoveToNotImportantAndNotUrgentTasksCommand.Execute();
                            break;
                        case "Unsigned":
                            viewModel.MoveToUnsignedTasksCommand.Execute();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Context_MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            TasksListViewModel viewModel = this.DataContext as TasksListViewModel;
            if(viewModel != null)
            {
                viewModel.EditTaskCommand.Execute();
            }
        }

        private void Context_MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            TasksListViewModel viewModel = this.DataContext as TasksListViewModel;
            if (viewModel != null)
            {
                viewModel.RemoveTaskCommand.Execute();
            }
        }

    }
}
