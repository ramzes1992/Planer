using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Prism.Commands;
using Planer.Views;
using Model;
using System.Collections.ObjectModel;
using Model.Repositories;

namespace Planer.ViewModels
{
    public class TreeViewModel : BaseViewModel
    {
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

        public TreeViewModel(Project currentProject)
        {
            Text = "Some Text";
        }
    }
}
