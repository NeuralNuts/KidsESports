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
using UI;

namespace Tekkies_Custom_PCs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Functions
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            winReports window = new winReports();
            window.ShowDialog();
        }

        private void btnResults_Click(object sender, RoutedEventArgs e)
        {
            winResults window = new winResults();
            window.ShowDialog();
        }

        private void btnTeams_Click(object sender, RoutedEventArgs e)
        {
            winTeams window = new winTeams();
            window.ShowDialog();
        }

        private void btnEvents_Click(object sender, RoutedEventArgs e)
        {
            winEvents window = new winEvents();
            window.ShowDialog();
        }

        private void btnGamesPlayed_Click(object sender, RoutedEventArgs e)
        {
            winGamesPlayed window = new winGamesPlayed();
            window.ShowDialog();
        }
        #endregion
    }
}
