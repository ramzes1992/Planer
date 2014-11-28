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

using Model.Repositories;
using Model;

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        TaskRepository _taskRepository = new TaskRepository();

        Task _currentTaskToEdit;

        public EditTaskWindow(Task taskToEdit)
        {
            this._currentTaskToEdit = taskToEdit;
            InitializeComponent();

            if (_currentTaskToEdit != null)
            {
                v_ComboBox_TaskPriority.SelectedIndex = _currentTaskToEdit.Priority.HasValue ? _currentTaskToEdit.Priority.Value : 4;
                v_ComboBox_TaskState.SelectedIndex = _currentTaskToEdit.State;
                v_TextBox_TaskText.Text = _currentTaskToEdit.Text;
                v_DatePicker_Deadline.SelectedDate = _currentTaskToEdit.Deadline;
            }
            else
            {
                DialogResult = false;
                Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(v_TextBox_TaskText.Text))
            {
                int? priority = v_ComboBox_TaskPriority.SelectedIndex < 4 ? v_ComboBox_TaskPriority.SelectedIndex : (int?)null;

                _currentTaskToEdit.Text = v_TextBox_TaskText.Text;
                _currentTaskToEdit.Priority = priority;
                _currentTaskToEdit.State = v_ComboBox_TaskState.SelectedIndex;
                _currentTaskToEdit.Deadline = v_DatePicker_Deadline.SelectedDate;

                _taskRepository.Edit(_currentTaskToEdit);

                this.DialogResult = true;
            }
        }

        private void v_TextBox_TaskText_TextChanged(object sender, TextChangedEventArgs e)
        {
            v_Button_Edit.IsEnabled = !string.IsNullOrWhiteSpace(v_TextBox_TaskText.Text);
        }
    }
}
