using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Model.Repositories;
using Planer.Views;
using Microsoft.Practices.Prism.Commands;

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
                if(value != _currentProject)
                {
                    _currentProject = value;
                    RaisePropertyChanged(() => CurrentProject);
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

        #endregion

        #region Members

        UserRepository _userRepository = new UserRepository();

        #endregion

        #region Commands

        #region Menu Open Project
        public DelegateCommand OpenProjectClick { get; set; }
        private void OpenProjectExecute()
        {
            ProjectChooserWindow view = new ProjectChooserWindow();
            ProjectViewModel viewModel = new ProjectViewModel(_currentUser, view);
            view.DataContext = viewModel;

            var result = view.ShowDialog();

            if(result ?? false)
            {
                this.CurrentProject = viewModel.SelectedProject;
            }
        }
        #endregion

        #region Menu New Project
        public DelegateCommand NewProjectClick { get; set; }
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

        #endregion

        #region Ctor
        public MainViewModel(string currentUserName)
        {
            _currentUser = _userRepository.GetUserByName(currentUserName);
            OpenProjectClick = new DelegateCommand(OpenProjectExecute);
            NewProjectClick = new DelegateCommand(NewProjectExecute);
        }

        #endregion

    }
}
