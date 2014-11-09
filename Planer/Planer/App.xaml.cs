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
#if !DEBUG
            LoginWindow loginWindow = new LoginWindow();
            LoginViewModel loginViewModel = new LoginViewModel(loginWindow);

            loginWindow.DataContext = loginViewModel;
#endif
            MainWindow mainWindow = new MainWindow();

#if DEBUG
            MainViewModel mainViewModel = new MainViewModel("ramzes");
            mainWindow.DataContext = mainViewModel;
            mainWindow.Show();
#else

            var result = loginWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {
                MainViewModel mainViewModel = new MainViewModel(loginViewModel.UserName);
                mainWindow.DataContext = mainViewModel;
                mainWindow.Show();
            }
            else
            {
                mainWindow.Close();
            }

#endif
        }
    }
}
