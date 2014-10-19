using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Planer.ViewModels;
using Planer.Views;
using System.Diagnostics;

namespace Planer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindow loginWindow = new LoginWindow();
            LoginWindowViewModel loginViewModel = new LoginWindowViewModel(loginWindow);

            loginWindow.DataContext = loginViewModel;

            MainWindow mainWindow = new MainWindow();

            var result = loginWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {
                MainWindowViewModel mainViewModel = new MainWindowViewModel(loginViewModel.UserName);
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
            }
            else
            {
                mainWindow.Close();
            }
        }
    }
}
