#region Imports
using System.Windows;
using UI;
#endregion

namespace UI
{
    public partial class MainWindow : Window
    {
        #region Var
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

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
