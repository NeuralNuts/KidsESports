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
using Data_Management;
using Data_Management.Models;

namespace UI
{
    /// <summary>
    /// Interaction logic for winBuildSelection.xaml
    /// </summary>
    public partial class winResults : Window
    {
        //Data object to manage DB interactions.
        DataAccess data = new DataAccess();
        //List to hold all the Build table records. 
        List<ResultView> result_list = new List<ResultView>();

        public winResults()
        {
            InitializeComponent();
            RefreshDataTable();
        }

        /// <summary>
        /// Retrives all the records from the DB and displays them in the data grid.
        /// </summary>
        private void RefreshDataTable()
        {
            result_list = data.GetAllResults();
            dgvResults.ItemsSource = result_list;
            dgvResults.Items.Refresh();
        }

        /// <summary>
        /// Event triggered by the new button is pressed to open the build creation form.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //Create new instance of the newBuild window.
            winNewResults window = new winNewResults();

            //Opens the new window in an if statement which looks for a true DialogResult return value when it is closed to determine if it runs the statement.
            //The code in this window will procees no further until the new Window is closed.. 
            if (window.ShowDialog() == true)
            {
                //Refreshes  the data table if the true result is recieved.
                RefreshDataTable();
            }
        }

        /// <summary>
        /// Event triggered by the close button is pressed to close the window.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
