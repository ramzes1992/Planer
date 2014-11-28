using Model;
using Model.Repositories;
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

namespace Planer.Views
{
    /// <summary>
    /// Interaction logic for EditNodeWindow.xaml
    /// </summary>
    public partial class EditNodeWindow : Window
    {
        private Node _currentNodeToEdit;
        NodeRepository _nodeRepository = new NodeRepository();

        public EditNodeWindow(Node nodeToEdit)
        {
            this._currentNodeToEdit = nodeToEdit;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_currentNodeToEdit != null)
            {
                this.v_TextBox_NodeText.Text = _currentNodeToEdit.Name;
                this.v_Slider.Value = _currentNodeToEdit.Progress;


                if (!_currentNodeToEdit.Tasks.Any() && !_currentNodeToEdit.Children.Any() && !_currentNodeToEdit.Accounts.Any())
                {
                    v_TextBlock.Visibility = System.Windows.Visibility.Visible;
                    v_Slider.Visibility = System.Windows.Visibility.Visible;
                    v_Label.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(v_TextBox_NodeText.Text))
            {
                _currentNodeToEdit.Name = v_TextBox_NodeText.Text;
                _currentNodeToEdit.Progress = (int)v_Slider.Value;
                _nodeRepository.Edit(_currentNodeToEdit);

                this.DialogResult = true;
            }
        }

        private void v_TextBox_NodeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            v_Button_Edit.IsEnabled = !string.IsNullOrWhiteSpace(v_TextBox_NodeText.Text);
        }
    }
}
