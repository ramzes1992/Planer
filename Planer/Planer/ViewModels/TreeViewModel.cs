﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Prism.Commands;
using Planer.Views;
using Model;
using System.Collections.ObjectModel;
using Model.Repositories;
using Planer.Models;

namespace Planer.ViewModels
{
    public class TreeViewModel : BaseViewModel
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

                    Text = GetParentsNames();//TODO: remove
                    //RefreshTasksCollection();
                }
            }
        }
        #endregion

        #region Tasks
        //private ObservableCollection<Task> Tasks { get; set;}
        #endregion

        #endregion

        #region Commands

        #region Context Menu Add New Node

        public DelegateCommand AddNewNodeCommand { get; set; }

        private void AddNewNodeExecute()
        {
            NewNodeWindow _view = new NewNodeWindow(_currentProject, SelectedItem);

            var result = _view.ShowDialog();

            if (result ?? false)
            {//result == true
                RefreshNodesCollection();
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
                RefreshNodesCollection();
            }
        }

        #endregion

        #region Context Menu Edit Node

        public DelegateCommand EditNodeCommand { get; set; }

        private void EditNodeExecute()
        {
            EditNodeWindow _view = new EditNodeWindow(SelectedItem);

            var result = _view.ShowDialog();

            if (result ?? false)
            {//result == true
                RefreshNodesCollection();
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

                RefreshNodesCollection();
            }
        }

        #endregion

        #endregion

        #region Members
        NodeRepository _nodeRepository = new NodeRepository();
        Project _currentProject;
        #endregion

        #region CTor
        public TreeViewModel(Project currentProject)
        {
            this._currentProject = currentProject;

            this.AddNewNodeCommand = new DelegateCommand(AddNewNodeExecute);
            this.AddNewRootNodeCommand = new DelegateCommand(AddNewRootNodeExecute);
            this.EditNodeCommand = new DelegateCommand(EditNodeExecute);
            this.RemoveNodeCommand = new DelegateCommand(RemoveNodeExecute);

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

        private string GetParentsNames()
        {
            if (SelectedItem != null)
            {
                var result = SelectedItem.Name;

                var parent = SelectedItem.Parent;

                while (parent != null)
                {
                    result += ";" + parent.Name;
                    parent = parent.Parent;
                }

                var itemsNames = result.Split(';').Reverse();
                return string.Join("  -->  ", itemsNames);
            }

            return string.Empty;
        }

        private void RefreshNodesCollection()
        {
            TopLevelNodes = new ObservableCollection<Node>(GetNodes());
            RaisePropertyChanged(() => TopLevelNodes);
        }

        //private void RefreshTasksCollection()
        //{
        //    if(SelectedItem != null)
        //    {
        //        Tasks = new ObservableCollection<Task>(SelectedItem.Tasks);
        //        RaisePropertyChanged(() => Tasks);
        //    }
        //}

        #endregion
    }
}
