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
using Universal;
using Model.Repositories;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for CollaboratorsWindow.xaml
    /// </summary>
    public partial class CollaboratorsWindow : Window
    {
        private User _currentUser;
        private Project _currentProject;

        private UserRepository _userRepo = new UserRepository();
        private ProjectRepository _projectRepo = new ProjectRepository();


        public CollaboratorsWindow(User currentUser, Project currentProject)
        {
            this._currentUser = currentUser;
            this._currentProject = currentProject;

            InitializeComponent();

            if (_currentUser.IsNull() || _currentProject.IsNull())
            {
                DialogResult = false;
                Close();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            v_ListBox_Collaborators.ItemsSource = _currentProject.Collaborators.Select(c => c.User);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (v_ListBox_Collaborators.SelectedItem != null && v_ListBox_Collaborators.SelectedItem is User)
            //{
                UserChooserWindow view = new UserChooserWindow(_currentUser, _currentProject);

                var result = view.ShowDialog();
                if (result ?? false)
                {
                    RefreshView();
                }
            //}
        }

        private void v_ListBox_Collaborators_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (v_ListBox_Collaborators.SelectedItem != null)
            //{
            //    v_Button_Add.IsEnabled = true;
            //}
            //else
            //{
            //    v_Button_Add.IsEnabled = false;
            //}
        }

        private void ListBoxItemContextMenu_RemoveCollaboratorClick(object sender, RoutedEventArgs e)
        {
            if (v_ListBox_Collaborators.SelectedItem != null
                && v_ListBox_Collaborators.SelectedItem is User)
            {
                var selectedUser = v_ListBox_Collaborators.SelectedItem as User;
                var collaboratorToRemove = _currentProject.Collaborators.SingleOrDefault(c => c.User.Equals(selectedUser));

                if (!collaboratorToRemove.IsNull())
                {
                    _currentProject.Collaborators.Remove(collaboratorToRemove);
                }

                RefreshView();
            }
        }

        private void RefreshView()
        {
            v_ListBox_Collaborators.ItemsSource = _currentProject.Collaborators.Select(c => c.User);
        }
    }
}
