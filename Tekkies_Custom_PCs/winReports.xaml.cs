using Data_Management;
using Data_Management.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Tekkies_Custom_PCs
{
    /// <summary>
    /// Interaction logic for winReports.xaml
    /// </summary>
    public partial class winReports : Window
    {
        //Databse connection object to manage all data interactions.
        DataAccess data = new DataAccess();
        //List of options for reports combo box selction.
        List<string> reportOptions = new List<string> { "Teams By Names", "Events By Name", "Games Played By Game Name" };

        //Lists to manage the filtering functionality of the Customer reports. The full list holds all the entire records when initially retrieved from database.
        //The filters list is the state of the collection after applying the current filters.
        //The displaylist is the list used by the Data Grid to display whichever of the 2 lists is currently being used. This list holds no data of its own and is just
        //used keep a reference of the currently desired list when it is assigned.
        List<Teams> fullTeamList;
        List<Teams> filteredTeamList = new List<Teams>();
        List<Teams> TeamDisplayList;

        //Lists to manage the filtering functionality of the Product reports. The full list holds all the entire records when initially retrieved from database.
        //The filters list is the state of the collection after applying the current filters.
        //The displaylist is the list used by the Data Grid to display whichever of the 2 lists is currently being used. This list holds no data of its own and is just
        //used keep a reference of the currently desired list when it is assigned.
        List<GamesPlayedView> fullGamesPlayedList;
        List<GamesPlayedView> filteredGamesPlayedList = new List<GamesPlayedView>();
        List<GamesPlayedView> displayGamesPlayedList;


        public winReports()
        {
            InitializeComponent();

            //Assigns the list of options to the combo box.
            cboType.ItemsSource = reportOptions;
 
        }

        /// <summary>
        /// Manages the functionality triggered when the option selected in the combo box is changed. 
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //If the combo box is on the empty selection, do nothing.
            //Otherwise, retrieve and filter the required data according to the desired selection. 
            if (cboType.SelectedIndex < 0)
            {
                return;
            }
            else if (cboType.SelectedIndex == 0)
            {
                //Retrieves the data from the database. 
                fullTeamList = data.GetTeams();
                //Using Linq, order the retrieved records by customer Last Name.
                fullTeamList = fullTeamList.OrderBy(s => s.TeamName).ToList();
                DisplayActiveCustomerList(fullTeamList);

            }
            else if (cboType.SelectedIndex == 1)
            {
                //Retrieves the data from the database. 
                fullGamesPlayedList = data.GetGamesPlayed();
                //Using Linq, order the retrieved records by Product Category.
                fullGamesPlayedList = fullGamesPlayedList.OrderBy(c => c.GameName).ToList();
                DisplayActiveProductList(fullGamesPlayedList);
            }
            else
            {
                //Retrieves the data from the database. 
                fullGamesPlayedList = data.GetGamesPlayed();
                //Using Linq, order the retrieved records in descending order by product Price. In this instance the price needs to be
                //changed from a string to integer after removing the dollar sign from the price.
                fullGamesPlayedList = fullGamesPlayedList.OrderByDescending(c => c.GameType.Remove(0,1)).ToList();
                DisplayActiveProductList(fullGamesPlayedList);
            }

            //Clear the text field.
            txtSearch.Text = "";
            
        }

        /// <summary>
        /// Display the product list on screen according to whichever version of the product list is desired(full or filtered) 
        /// </summary>
        /// <param name="activeList"> The list to be shown in the Data Grid</param>
        private void DisplayActiveProductList(List<GamesPlayedView> activeList)
        {
            displayGamesPlayedList = activeList;
            dgvReport.ItemsSource = displayGamesPlayedList;
            dgvReport.Items.Refresh();
        }

        /// <summary>
        /// Event triggered when the Export button is pressed.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            //If no eport type is selected, do nothing and return.
            if (cboType.SelectedIndex < 0)
            {
                return;
            }

            //Create save dialog object. This will eventually open the Windows file chooser to allow file name and location selection.
            SaveFileDialog saveDialog = new SaveFileDialog();
            //Sets the filters for the allowed file types of the dialog.
            //These are passed as a string in the following format: {File Description}|{extension type} with each fileter separated by a new pipe(|) character.
            saveDialog.Filter = "Simple Text File(.txt)|*.txt|" +
                                "Comma Separated Values (.csv)|*.csv";
            //Sets the initial file directory when the dialog first opens. This version retrieves the filepath for the desktop directory.
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //If the dialog returns a true message(OK/SAVE button pressed) proceed with the saving procedure.
            //The Show dialog process wihtin the if statement, opens the file chooser.
            if (saveDialog.ShowDialog() == true)
            {
                //Save the data in comma separated format based uponn whether the selecter is on the customer(0 index) or product ( 1+ indexes).
                if (cboType.SelectedIndex == 0)
                {
                    //Create stream writer to manage writing to file.
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        //Iterate through each item in the displayed customer list and write them to file.
                        foreach (var item in TeamDisplayList)
                        {
                            writer.WriteLine($"{item.TeamID},{item.TeamName},{item.PrimaryContact},{item.ContactEmail},{item.ContactPhone},{item.CompetitionPoints}");
                        }
                    }
                }
                else
                {
                    //Create stream writer to manage writing to file.
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        //Iterate through each item in the displayed customer list and write them to file.
                        foreach (var item in displayGamesPlayedList)
                        {
                            writer.WriteLine($"{item.GamesPlayedID},{item.GameName},{item.GameType},{item.TeamName}");
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Event which triggers when the text in the search text field changes.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //If no report type is selected, do nothing.
            if (cboType.SelectedIndex < 0)
            {
                return;
            }

            //If the customer option is selected in the combo box (Index 0), filter the customer list.
            if (cboType.SelectedIndex == 0 )
            {
                //If no text is in the search field, display the full list.
                if (String.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    DisplayActiveCustomerList(fullTeamList);
                    return;
                }

                //Filter list using Linq to copy all the customers who's first or last name's contain contents of the search field.
                filteredTeamList = fullTeamList.Where(f => f.TeamName.ToUpper().Contains(txtSearch.Text.ToUpper())
                                                           || f.ContactEmail.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();


                //NOTE: The commented sections below are the alternative to filtering the customer list if not using Linq

                //filteredCustomerList.Clear(); 

                //foreach (var person in fullCustomerList)
                //{
                //    if (person.FirstName.ToUpper().Contains(txtSearch.Text.ToUpper()) || 
                //                person.LastName.ToUpper().Contains(txtSearch.Text.ToUpper()))
                //    {
                //        filteredCustomerList.Add(person);
                //    }
                //}

                DisplayActiveCustomerList(filteredTeamList);
            }

            //If one of the product options is selected in the combo box (Indexes 1+), filter the product list.
            if (cboType.SelectedIndex > 0)
            {
                //If no text is in the search field, display the full list.
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    DisplayActiveProductList(fullGamesPlayedList);
                    return;
                }
                //Filter list using Linq to copy all the products whos  names or description field contains contents of the search field.
                filteredGamesPlayedList = fullGamesPlayedList.Where(p => p.GameName.ToUpper().Contains(txtSearch.Text.ToUpper()) ||
                                                              p.GameType.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();
                DisplayActiveProductList(filteredGamesPlayedList);
            }
            

        }

        /// <summary>
        /// Display the customers list on screen according to whichever version of the customer list is desired(full or filtered) 
        /// </summary>
        /// <param name="activeList"> The list to be shown in the Data Grid</param>
        private void DisplayActiveCustomerList(List<Teams> activeList)
        {
            TeamDisplayList = activeList;
            dgvReport.ItemsSource = TeamDisplayList;
            dgvReport.Items.Refresh();
        }

        /// <summary>
        /// Event when close button is pressed to close the window.
        /// </summary>
        /// <param name="sender">The object triggering the event</param>
        /// <param name="e">Any paramaters passed when the event is triggered by its component</param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
    }
}
