using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using Planer.Views;
using System.Collections.ObjectModel;
using Model.Repositories;

namespace Planer.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        #region Properties

        #region Projects

        public ObservableCollection<Project> Projects { get; set; }

        #endregion

        #region SheredProjects
        public ObservableCollection<Project> SheredProjects { get; set; }
        #endregion

        #region Selected Project
        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                if (value != _selectedProject)
                {
                    _selectedProject = value;
                    RaisePropertyChanged(() => SelectedProject);
                    OpenProjectCommand.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #region NewProjectName

        private string _newProjectName;
        public string NewProjectName
        {
            get
            {
                return _newProjectName;
            }
            set
            {
                if (value != _newProjectName)
                {
                    _newProjectName = value;
                    RaisePropertyChanged(() => NewProjectName);
                    CreateNewProjectCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #endregion

        #region Members

        User _currentUser;

        Window _view;

        ProjectRepository _projectRepository = new ProjectRepository();

        #endregion

        #region Commands

        #region
        private void InitializeCommands()
        {
            OpenProjectCommand = new DelegateCommand(OpenProjectExecute, OpenProjectCanExecute);
            CreateNewProjectCommand = new DelegateCommand(CreateNewProjectExecute, CreateNewProjectCanExecute);
        }
        #endregion

        #region Open Project Command
        public DelegateCommand OpenProjectCommand { get; set; }

        private bool OpenProjectCanExecute()
        {
            return SelectedProject != null;
        }

        private void OpenProjectExecute()
        {
            _view.DialogResult = true;
            _view.Close();
        }

        #endregion

        #region Create Project Command

        public DelegateCommand CreateNewProjectCommand { get; set; }

        private bool CreateNewProjectCanExecute()
        {
            return !string.IsNullOrWhiteSpace(NewProjectName);
        }

        private void CreateNewProjectExecute()
        {
            Project newProject = new Project();
            newProject.Name = NewProjectName;
            newProject.Owner = _currentUser;

            _projectRepository.Add(newProject);

            Projects.Add(newProject);

            SelectedProject = newProject;

            NewProjectName = string.Empty;

            _view.DialogResult = true;
            _view.Close();

        }

        #endregion

        #endregion

        #region Ctor
        public ProjectViewModel(User user, Window view)
        {
            this._currentUser = user;
            this._view = view;

            InitializeCommands();

            Projects = new ObservableCollection<Project>(_currentUser.Projects);
            SheredProjects = new ObservableCollection<Project>(_currentUser.Collaborators.Select(c => c.Project));
        }
        #endregion

    }
}
