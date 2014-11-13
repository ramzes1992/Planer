using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Model;
using Model.Enums;
using Model.Repositories;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for NewTaskWindow.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        TaskRepository _taskRepository = new TaskRepository();
        Project _currentProject;
        Node _currentNode;
        public NewTaskWindow(Project currentProject, EisenhowerPriority? startUpPriority = null)
        {
            this._currentProject = currentProject;
            InitializeComponent();
        }

        public NewTaskWindow(Project currentProject, Node currentNode)
        {
            this._currentProject = currentProject;
            this._currentNode = currentNode;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(v_TextBox_TaskText.Text))
            {
                int? priority = v_ComboBox_TaskPriority.SelectedIndex < 4 ? v_ComboBox_TaskPriority.SelectedIndex : (int?)null;

                var task = new Task()
                {
                    Text = v_TextBox_TaskText.Text,
                    Project = _currentProject,
                    Node = _currentNode,
                    Priority = priority
                };

                _taskRepository.Add(task);

                this.DialogResult = true;
            }
        }

        private void v_TextBox_TaskText_TextChanged(object sender, TextChangedEventArgs e)
        {
            v_Button_Add.IsEnabled = !string.IsNullOrWhiteSpace(v_TextBox_TaskText.Text);
        }
    }
}
