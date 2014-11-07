using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Collections.ObjectModel;
using Model.Repositories;
using Model.Enums;

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
                if(value != _selectedItem)
                {
                    _selectedItem = value;
                    RaisePropertyChanged(() => SelectedItem);
                }
            }
        }
        #endregion

        #endregion

        #region Members

        TaskRepository _taskRepository = new TaskRepository();

        Project _currentProject;

        #endregion

        #region Ctor
        public TasksListViewModel(Project currentProject)
        {
            this._currentProject = currentProject;

            ImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndUrgent));
            ImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.ImportantAndNotUrgent));
            NotImportantAndUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndUrgent));
            NotImportantAndNotUrgentTasks = new ObservableCollection<Task>(GetTasks(EisenhowerPriority.NotImportantAndNotUrgent));
            UnsignedTasks = new ObservableCollection<Task>(GetTasks());

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
        {//TODO: ??

        }

        #endregion

    }
}
