using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Commands;
using Planer.Views;
using Model;
using System.Collections.ObjectModel;
using Model.Repositories;
using Planer.Models;
using Model.Enums;

namespace Planer.ViewModels
{
    public class TreeViewModel : BaseViewModel, IPage
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

        #region Nodes

        public ObservableCollection<Node> TopLevelNodes { get; set; }

        #endregion

        #region SelectedItem
        private Node _selectedItem;
        public Node SelectedItem
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
                    AddTaskToNodeCommand.RaiseCanExecuteChanged();

                    if(_selectedItem != null)
                    {
                        AssignedMoneyboxes = new ObservableCollection<AccountModel>(GetCurrentNodeMoneyboxes());
                        RaisePropertyChanged(() => AssignedMoneyboxes);
                    }

                    Text = GetParentsNames();//TODO: remove
                    //RefreshTasksCollection();
                }
            }
        }
        #endregion

        #region SelectedTask

        private Task _selectedTask;
        public Task SelectedTask
        {
            get
            {
                return _selectedTask;
            }

            set
            {
                if (value != _selectedTask)
                {
                    _selectedTask = value;
                    RaisePropertyChanged(() => SelectedTask);
                }
            }
        }

        #endregion

        #region SelectedMoneybox

        private AccountModel _selectedMoneybox;
        public AccountModel SelectedMoneybox
        {
            get
            {
                return _selectedMoneybox;
            }

            set
            {
                if (value != _selectedMoneybox)
                {
                    _selectedMoneybox = value;
                    RaisePropertyChanged(() => SelectedMoneybox);
                }
            }
        }

        #endregion

        #region AssignedMoneyboxes

        public ObservableCollection<AccountModel> AssignedMoneyboxes { get; set; }

        #endregion

        #region Tasks
        //private ObservableCollection<Task> Tasks { get; set;}
        #endregion

        #endregion

        #region Commands

        #region Initialize Commands

        private void InitializeCommands()
        {
            this.AddNewNodeCommand = new DelegateCommand(AddNewNodeExecute);
            this.AddNewRootNodeCommand = new DelegateCommand(AddNewRootNodeExecute);
            this.EditNodeCommand = new DelegateCommand(EditNodeExecute);
            this.RemoveNodeCommand = new DelegateCommand(RemoveNodeExecute);

            this.AddTaskToNodeCommand = new DelegateCommand(AddTaskToNodeExecute, AddTaskToNodeCanExecute);
            this.EditTaskCommand = new DelegateCommand(EditTaskExecute);
            this.RemoveTaskCommand = new DelegateCommand(RemoveTaskExecute);

            this.AssignMoneyboxCommand = new DelegateCommand(AssignMoneyboxExecute);
            this.UnassignMoneyboxCommand = new DelegateCommand(UnassignMoneyboxExecute);
        }

        #endregion

        #region Context Menu Add New Node

        public DelegateCommand AddNewNodeCommand { get; set; }

        private void AddNewNodeExecute()
        {
            if (SelectedItem != null)
            {
                NewNodeWindow _view = new NewNodeWindow(_currentProject, SelectedItem);

                var result = _view.ShowDialog();

                if (result ?? false)
                {//result == true
                    Refresh();
                }
            }
        }

        #endregion

        #region Context Menu Add New Node to root

        public DelegateCommand AddNewRootNodeCommand { get; set; }

        private void AddNewRootNodeExecute()
        {
            NewNodeWindow _view = new NewNodeWindow(_currentProject);

            var result = _view.ShowDialog();

            if (result ?? false)
            {//result == true
                Refresh();
            }
        }

        #endregion

        #region Context Menu Edit Node

        public DelegateCommand EditNodeCommand { get; set; }

        private void EditNodeExecute()
        {
            if (SelectedItem != null)
            {
                EditNodeWindow _view = new EditNodeWindow(SelectedItem);

                var result = _view.ShowDialog();

                if (result ?? false)
                {//result == true
                    Refresh();
                }
            }
        }

        #endregion

        #region Context Menu Remove Selected Node

        public DelegateCommand RemoveNodeCommand { get; set; }

        private void RemoveNodeExecute()
        {
            if (SelectedItem != null)
            {
                _nodeRepository.Remove(SelectedItem);
                SelectedItem = null;

                Refresh();
            }
        }

        #endregion

        #region Context Menu Add New Task To Selected Node

        public DelegateCommand AddTaskToNodeCommand { get; set; }

        private void AddTaskToNodeExecute()
        {
            if (SelectedItem != null)
            {
                NewTaskWindow _view = new NewTaskWindow(_currentProject, SelectedItem);
                var result = _view.ShowDialog();

                if (result ?? false)
                {
                    Refresh();
                }
            }
        }

        private bool AddTaskToNodeCanExecute()
        {
            return SelectedItem != null;
        }

        #endregion

        #region Context Menu Edit Task

        public DelegateCommand EditTaskCommand { get; set; }

        private void EditTaskExecute()
        {
            if (SelectedTask != null)
            {
                EditTaskWindow _view = new EditTaskWindow(SelectedTask);
                var result = _view.ShowDialog();

                if (result ?? false)
                {
                    Refresh();
                    
                }
            }
        }

        #endregion

        #region Context Menu Remove Task

        public DelegateCommand RemoveTaskCommand { get; set; }

        private void RemoveTaskExecute()
        {
            if (SelectedTask != null)
            {
                _taskRepository.Remove(SelectedTask);
                Refresh();
            }

        }

        #endregion

        #region Context Menu Assign Moneybox
        public DelegateCommand AssignMoneyboxCommand { get; set; }

        private void AssignMoneyboxExecute()
        {
            if (SelectedItem != null)
            {
                MoneyboxesChooserWindow view = new MoneyboxesChooserWindow(_currentProject, SelectedItem);
                var result = view.ShowDialog();

                if(result ?? false)
                {
                    Refresh();
                }
            }
        }
        #endregion

        #region Context Menu Unassign Moneybox
        public DelegateCommand UnassignMoneyboxCommand { get; set; }

        private void UnassignMoneyboxExecute()
        {
            if (SelectedMoneybox != null)
            {
                var moneybox = _accountRepository.GetById(SelectedMoneybox.Id);
                moneybox.Node = null;
                _accountRepository.Edit(moneybox);

                Refresh();
            }
        }
        #endregion

        #endregion

        #region Members
        NodeRepository _nodeRepository = new NodeRepository();
        TaskRepository _taskRepository = new TaskRepository();
        AccountRepository _accountRepository = new AccountRepository();
        Project _currentProject;
        #endregion

        #region CTor
        public TreeViewModel(Project currentProject)
        {
            this._currentProject = currentProject;

            InitializeCommands();

            if (_currentProject != null)
            {
                TopLevelNodes = new ObservableCollection<Node>(GetNodes());
            }
        }

        #endregion

        #region Private Methods

        private IQueryable<Node> GetNodes(Node root = null)
        {
            return _nodeRepository.GetAll().Where(n => n.ProjectId.Equals(_currentProject.Id)
                                                            && n.Parent == root);
        }

        private IEnumerable<AccountModel> GetCurrentNodeMoneyboxes()
        {
            return SelectedItem.Accounts.Select(a => new AccountModel()
            {
                Id = a.Id,
                Name = a.Name,
                Amount = _accountRepository.GetAmount(a),
                Type = (AccountType)a.Type
            });
        }

        private string GetParentsNames()
        {
            if (SelectedItem != null)
            {
                var result = SelectedItem.Name;

                if (!string.IsNullOrEmpty(result))
                {
                    var parent = SelectedItem.Parent;

                    while (parent != null)
                    {
                        result += ";" + parent.Name;
                        parent = parent.Parent;
                    }

                    var itemsNames = result.Split(';').Reverse();
                    return string.Join("  -->  ", itemsNames);
                }
            }

            return string.Empty;
        }

        private void RefreshNodesCollection()
        {
            TopLevelNodes = new ObservableCollection<Node>(GetNodes());
            RaisePropertyChanged(() => TopLevelNodes);
        }

        private void RefreshSelectedNode()
        {
            if (SelectedItem != null)
            {
                var id = SelectedItem.Id;
                SelectedItem = new Node();
                SelectedItem = _nodeRepository.GetById(id);
            }
        }

        #endregion

        public void Refresh()
        {
            RefreshNodesCollection();
            RefreshSelectedNode();
        }
    }
}
