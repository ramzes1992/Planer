using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Planer.ViewModels
{
    public class TasksListViewModel : BaseViewModel
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
                if(_text != value)
                {
                    _text = value;
                    RaisePropertyChanged(() => Text);
                }
            }
        }

        public TasksListViewModel(Project currentProject)
        {
            Text = "Some Text";
        }

    }
}
