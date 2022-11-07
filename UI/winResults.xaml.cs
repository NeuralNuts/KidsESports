#region Imports
using System.Collections.Generic;
using System.Windows;
using Data_Management;
using Data_Management.Models;
#endregion

namespace UI
{
    public partial class winResults : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<ResultView> result_list = new List<ResultView>();

        public winResults()
        {
            InitializeComponent();
            RefreshDataTable();
        }
        #endregion

        #region Functions
        private void RefreshDataTable()
        {
            result_list = data.GetAllResults();
            dgvResults.ItemsSource = result_list;
            dgvResults.Items.Refresh();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            winNewResults window = new winNewResults();

            if (window.ShowDialog() == true)
            {
                RefreshDataTable();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
