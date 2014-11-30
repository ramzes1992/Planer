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
    /// Interaction logic for UserChooserWindow.xaml
    /// </summary>
    public partial class UserChooserWindow : Window
    {
        private User _currentUser;
        private Project _currentProject;

        private UserRepository _userRepo = new UserRepository();
        private ProjectRepository _projectRepo = new ProjectRepository();

        public UserChooserWindow(User currentUser, Project currentProject)
        {
            this._currentProject = currentProject;
            this._currentUser = currentUser;

            if (_currentUser.IsNull() || _currentProject.IsNull())
            {
                DialogResult = false;
                Close();
            }

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var excludedUsers = _currentProject.Collaborators.Select(c => c.User.Id).ToList();
            excludedUsers.Add(_currentUser.Id);

            v_ListBox_Users.ItemsSource = _userRepo.GetAll().Where(u => !excludedUsers.Contains(u.Id)).ToArray();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (v_ListBox_Users.SelectedItem != null && v_ListBox_Users.SelectedItem is User)
            {
                _currentProject.Collaborators.Add(new Collaborator()
                {
                    User = v_ListBox_Users.SelectedItem as User
                });

                _projectRepo.Edit(_currentProject);

                DialogResult = true;
                Close();
            }
        }

        private void v_ListBox_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (v_ListBox_Users.SelectedItem != null)
            {
                v_Button_Add.IsEnabled = true;
            }
            else
            {
                v_Button_Add.IsEnabled = false;
            }
        }
    }
}
