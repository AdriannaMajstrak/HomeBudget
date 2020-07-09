using HomeBudget.Client.ViewModel;
using HomeBudget.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HomeBudget.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                BudgetEntities budgetEntities = new BudgetEntities();

                MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(budgetEntities);

                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = mainWindowViewModel;
                mainWindow.ShowDialog();

            }
            catch (System.Data.Entity.Core.EntityException)
            {

                MessageBox.Show("Nie ma połaczenia z bazą danych. Aplikacja zostanie zamknięta.", "Błąd krytyczny", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(100);
            }

    

        }
    }
}
