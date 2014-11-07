using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Model.Repositories;
using Planer.Views;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;

namespace Planer.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties

        #region Current User
        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }

            set
            {
                if (value != _currentUser)
                {
                    _currentUser = value;
                    RaisePropertyChanged(() => CurrentUser);
                }
            }
        }
        #endregion

        #region Current Project

        private Project _currentProject;
        public Project CurrentProject
        {
            get
            {
                return _currentProject;
            }
            set
            {
                if (value != _currentProject)
                {
                    var isOldValueNull = _currentProject == null;

                    _currentProject = value;
                    RaisePropertyChanged(() => CurrentProject);

                    if (isOldValueNull)
                    {//selected new project
                        NavigateToTreePageExecute();
                    }

                    if (value == null)
                    {//project is closed
                        CurrentPage = null;
                    }
                }
            }
        }

        //private string _projectName;
        //public string ProjectName
        //{
        //    get
        //    {
        //        return _projectName;
        //    }
        //    set
        //    {
        //        if(value != _projectName)
        //        {
        //            _projectName = value;
        //            RaisePropertyChanged(() => ProjectName);
        //        }
        //    }
        //}
        #endregion

        #region Current Page

        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    RaisePropertyChanged(() => CurrentPage);
                }
            }
        }

        #endregion

        #endregion

        #region Members

        UserRepository _userRepository = new UserRepository();

        #endregion

        #region Commands

        #region Menu Open Project
        public DelegateCommand OpenProjectCommand { get; set; }
        private void OpenProjectExecute()
        {
            ProjectChooserWindow view = new ProjectChooserWindow();
            ProjectViewModel viewModel = new ProjectViewModel(_currentUser, view);
            view.DataContext = viewModel;

            var result = view.ShowDialog();

            if (result ?? false)
            {
                this.CurrentProject = viewModel.SelectedProject;
            }
        }
        #endregion

        #region Menu New Project
        public DelegateCommand NewProjectCommand { get; set; }
        private void NewProjectExecute()
        {
            NewProjectWindow view = new NewProjectWindow();
            ProjectViewModel viewModel = new ProjectViewModel(_currentUser, view);
            view.DataContext = viewModel;

            var result = view.ShowDialog();

            if (result ?? false)
            {
                this.CurrentProject = viewModel.SelectedProject;
            }
        }
        #endregion

        #region Menu Close Project
        public DelegateCommand CloseProjectCommand { get; set; }
        private void CloseProjectExecute()
        {
            CurrentProject = null;
        }
        #endregion

        #region Navigate To Tree Page
        public DelegateCommand NavigateToTreePageCommand { get; set; }

        private void NavigateToTreePageExecute()
        {
            Page view = new TreePage();
            TreeViewModel _treeViewModel = new TreeViewModel(CurrentProject);
            view.DataContext = _treeViewModel;
            CurrentPage = view;
        }
        #endregion

        #region Navigate To Tasks List Page
        public DelegateCommand NavigateToTasksListPageCommand { get; set; }

        private void NavigateToTasksListPageExecute()
        {
            Page view = new TasksListPage();
            TasksListViewModel _tasksListViewModel = new TasksListViewModel(CurrentProject);
            view.DataContext = _tasksListViewModel;
            CurrentPage = view;
        }
        #endregion

        #region Navigate To Budget Page
        public DelegateCommand NavigateToBudgetPageCommand { get; set; }

        private void NavigateToBudgetPageExecute()
        {
            Page view = new BudgetPage();
            BudgetViewModel _budgetViewModel = new BudgetViewModel(CurrentProject);
            view.DataContext = _budgetViewModel;
            CurrentPage = view;
        }
        #endregion

        #endregion

        #region Ctor
        public MainViewModel(string currentUserName)
        {
            _currentUser = _userRepository.GetUserByName(currentUserName);

            OpenProjectCommand = new DelegateCommand(OpenProjectExecute);
            NewProjectCommand = new DelegateCommand(NewProjectExecute);
            CloseProjectCommand = new DelegateCommand(CloseProjectExecute);

            NavigateToTreePageCommand = new DelegateCommand(NavigateToTreePageExecute);
            NavigateToTasksListPageCommand = new DelegateCommand(NavigateToTasksListPageExecute);
            NavigateToBudgetPageCommand = new DelegateCommand(NavigateToBudgetPageExecute);

        }

        #endregion

    }
}
