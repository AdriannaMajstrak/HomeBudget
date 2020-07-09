using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Threading;

namespace HomeBudget.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ScrollNav.SizeChanged += (s, o) =>
            {
                if(ScrollNav.ActualHeight != NavGrid.ActualHeight)
                {
                    BtnNav.Visibility = Visibility.Visible;
                    BtnNavUp.Visibility = Visibility.Visible;
                }
                else
                {
                    BtnNav.Visibility = Visibility.Hidden;
                    BtnNavUp.Visibility = Visibility.Hidden;
                }
            };
        }

        bool gridIsOpen;

        private void openMenu()
        {
            MainWindowGrid.ColumnDefinitions[0].Width = new GridLength(300);
            Properties.Settings.Default["ActualSettlementPeriodId"] = 0;
            var t = Properties.Settings.Default["ActualSettlementPeriodId"];
            Properties.Settings.Default.Save();
        }

        private void closeMenu()
        {
            MainWindowGrid.ColumnDefinitions[0].Width = new GridLength(75);
        }

        private void OpenOrCloseGrid(object sender, RoutedEventArgs e)
        {
            if(gridIsOpen)
            {
                closeMenu();
            }
            else
            {
                openMenu();
            }

            gridIsOpen = !gridIsOpen;

        }

        private void BtnNavDownClick(object sender, RoutedEventArgs e)
        {
            ScrollNav.ScrollToVerticalOffset(ScrollNav.VerticalOffset + 50);
        }

        private void BtnNavUpClick(object sender, RoutedEventArgs e)
        {
            ScrollNav.ScrollToVerticalOffset(ScrollNav.VerticalOffset - 50);
        }
    }
}
