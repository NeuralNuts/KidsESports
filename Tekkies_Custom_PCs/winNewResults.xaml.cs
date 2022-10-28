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

namespace UI
{
    /// <summary>
    /// Interaction logic for winNewBuild.xaml
    /// </summary>
    public partial class winNewResults : Window
    {
        public winNewResults()
        {
            InitializeComponent();
        }

        /// Event triggered when the Save button is pressed save the new build record and return a DialogResult of true to the previous window.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Save logic goes here


            //Sets DialogResult and closes window.
            DialogResult = true;
            Close();
        }
    }
}
