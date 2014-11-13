using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Commands;
using Model;
using System.Collections.ObjectModel;
using Model.Repositories;
using Model.Enums;
using Planer.Views;

namespace Planer.ViewModels
{
    public class TasksListViewModel : BaseViewModel
    {
        #region Properties

        #region Text
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text != value)
                {
                    _text = value;
                    RaisePropertyChanged(() => Text);
                }
            }
        }
        #endregion

        #region Task Lists

        public ObservableCollection<Task> ImportantAndUrgentTasks { get; set; }
        public ObservableCollection<Task> ImportantAndNotUrgentTasks { get; set; }
        public ObservableCollection<Task> NotImportantAndUrgentTasks { get; set; }
        public ObservableCollection<Task> NotImportantAndNotUrgentTasks { get; set; }
        public ObservableCollection<Task> UnsignedTasks { get; set; }

        #endregion

        #region Selected Task

        private Task _selectedItem;
        public Task SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    RaisePropertyChanged(() => SelectedItem);
                }
            }
        }
        #endregion

        #endregion

        #region Commands

        public DelegateCommand MoveToImportantAndUrgentTasksCommand { get; set; }
        private void MoveToImportantAndUrgentTasksExecute()
        {
            _taskRepository.ChangePriority(SelectedItem, EisenhowerPriority.ImportantAndUrgent);
            RefreshLists();
        }

        public DelegateCommand MoveToImportantAndNotUrgentTasksCommand { get; set; }
        private void MoveToImportantAndNotUrgentTasksExecute()
        {
            _taskRepository.ChangePriority(SelectedItem, EisenhowerPriority.ImportantAndNotUrgent);
            RefreshLists();
        }

        public DelegateCommand MoveToNotImportantAndUrgentTasksCommand { get; set; }
        private void MoveToNotImportantAndUrgentTasksExecute()
        {
            _taskRepository.ChangePriority(SelectedItem, EisenhowerPriority.NotImportantAndUrgent);
            RefreshLists();
        }

        public DelegateCommand MoveToNotImportantAndNotUrgentTasksCommand { get; set; }
        private void MoveToNotImportantAndNotUrgentTasksExecute()
        {
            _taskRepository.ChangePriority(SelectedItem, EisenhowerPriority.NotImportantAndNotUrgent);
            RefreshLists();
        }

        public DelegateCommand MoveToUnsignedTasksCommand { get; set; }
        private void MoveToUnsignedTasksExecute()
        {
            _taskRepository.ChangePriority(SelectedItem, null);
            RefreshLists();
        }

        public DelegateCommand AddNewTaskCommand { get; set; }
        private void AddNewTaskExecute()
        {
            NewTaskWindow _view = new NewTaskWindow(_currentProject);
            var result = _view.ShowDialog();

            if (result ?? false)
            {
                RefreshLists();
            }
        }

        public DelegateCommand EditTaskCommand { get; set; }
        private void EditTaskExecute()
        {
            EditTaskWindow _view = new EditTaskWindow(SelectedItem);
            var result = _view.ShowDialog();

            if (result ?? false)
            {
                RefreshLists();
            }
        }

        public DelegateCommand RemoveTaskCommand { get; set; }

        private void RemoveTaskExecute()
        {
            _taskRepository.Remove(SelectedItem);
            RefreshLists();
        }

        #endregion

        #region Members

        TaskRepository _taskRepository = new TaskRepository();

        Project _currentProject;

        #endregion

        #region Ctor
        public TasksListViewModel(Project currentProject)
        {
            this._currentProject = currentProject;

            this.ImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndUrgent));
            this.ImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndNotUrgent));
            this.NotImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndUrgent));
            this.NotImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndNotUrgent));
            this.UnsignedTasks = new ObservableCollection<Task>(GetTasks());


            this.MoveToImportantAndUrgentTasksCommand = new DelegateCommand(MoveToImportantAndUrgentTasksExecute);
            this.MoveToImportantAndNotUrgentTasksCommand = new DelegateCommand(MoveToImportantAndNotUrgentTasksExecute);
            this.MoveToNotImportantAndUrgentTasksCommand = new DelegateCommand(MoveToNotImportantAndUrgentTasksExecute);
            this.MoveToNotImportantAndNotUrgentTasksCommand = new DelegateCommand(MoveToNotImportantAndNotUrgentTasksExecute);

            this.MoveToUnsignedTasksCommand = new DelegateCommand(MoveToUnsignedTasksExecute);

            this.AddNewTaskCommand = new DelegateCommand(AddNewTaskExecute);
            this.EditTaskCommand = new DelegateCommand(EditTaskExecute);
            this.RemoveTaskCommand = new DelegateCommand(RemoveTaskExecute);
        }
        #endregion

        #region Private Methods

        private IQueryable<Task> GetTasks()
        {
            if (_currentProject != null)
            {
                return _taskRepository.GetAll()
                                      .Where(t => t.ProjectId.Equals(_currentProject.Id)
                                      && t.Priority == null);
            }

            return null;
        }

        private IQueryable<Task> GetTasks(EisenhowerPriority priority)
        {
            if (_currentProject != null)
            {
                return _taskRepository.GetAll().Where(t => t.ProjectId.Equals(_currentProject.Id)
                                                           && t.Priority == (int)priority);
            }

            return null;
        }

        private void RefreshLists()
        {
            ImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndUrgent));
            RaisePropertyChanged(() => ImportantAndUrgentTasks);
            ImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndNotUrgent));
            RaisePropertyChanged(() => ImportantAndNotUrgentTasks);
            NotImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndUrgent));
            RaisePropertyChanged(() => NotImportantAndUrgentTasks);
            NotImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndNotUrgent));
            RaisePropertyChanged(() => NotImportantAndNotUrgentTasks);
            UnsignedTasks = new ObservableCollection<Task>(GetTasks());
            RaisePropertyChanged(() => UnsignedTasks);
        }

        #endregion
    }
}
