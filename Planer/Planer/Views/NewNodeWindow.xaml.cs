using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for NewNodeWindow.xaml
    /// </summary>
    public partial class NewNodeWindow : Window
    {
        NodeRepository _nodeRepository = new NodeRepository();

        Project _currentProject;
        public NewNodeWindow(Project curretnProject, Node parent = null)
        {
            InitializeComponent();
            this._currentProject = curretnProject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(v_TextBox_NodeText.Text))
            {
                _nodeRepository.Add(v_TextBox_NodeText.Text, _currentProject);
                this.DialogResult = true;
            }
        }

        private void v_TextBox_NodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            v_Button_Add.IsEnabled = !string.IsNullOrWhiteSpace(v_TextBox_NodeText.Text);
        }
    }
}
