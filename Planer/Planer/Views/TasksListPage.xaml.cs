using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            ListBoxItem listBoxItem = ViewHelper.VisualUpwardSearch<ListBoxItem>(e.OriginalSource as DependencyObject);

            if (listBoxItem != null)
            {
                listBoxItem.Focus();
                listBoxItem.IsSelected = true;
                e.Handled = true;
            }
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

        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private void v_ListView_ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var listView = Helpers.ViewHelper.VisualUpwardSearch<ListView>(e.OriginalSource as DependencyObject);

            ColumnHeader_Click(sender, e, listView);
        }

        private void ColumnHeader_Click(object sender, RoutedEventArgs e, ListView listView)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                listView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            listView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }

    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                    (
                            AdornedElement.RenderSize.Width - 15,
                            (AdornedElement.RenderSize.Height - 5) / 2
                    );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}
