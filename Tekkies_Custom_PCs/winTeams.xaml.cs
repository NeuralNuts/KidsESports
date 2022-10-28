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
    /// Interaction logic for winCustomers.xaml
    /// </summary>
    public partial class winTeams : Window
    {
        DataAccess data = new DataAccess();
        List<Teams> team_list = new List<Teams>();
        Teams teams = new Teams();
        //Enumeration used to act as a trigger/selector to specify the current type of save to be used.
        //NOTE: See SaveMode enum for details.
        SaveMode saveType = SaveMode.NewSave;

        public winTeams()
        {
            InitializeComponent();
            UpdateDataGrid();  
        }

        /// <summary>
        /// Sets up the data grid to display all the records of Products list.
        /// </summary>
        private void UpdateDataGrid()
        {
            //Retrieves all Customer records from the databse and stores them in the list.
            team_list = data.GetTeams();
            //Sets the data source of the data grid to be based uopn the provided list.
            dgvTeams.ItemsSource = team_list;
            //Refreshes and updates the display of the data grid.
            dgvTeams.Items.Refresh();
        }

        /// <summary>
        /// Event triggered when the close button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Event triggered when the selected record of the data grid is changed by either selecting or unselecting a row. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void DgvTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvTeams.SelectedIndex < 0)
            {
                return;
            }
            //Retrieves the primary key from the currently selected row
            int id = team_list[dgvTeams.SelectedIndex].TeamID;
            //Retrieves the desired record matching the selected id.
            teams = data.GetTeamsById(id);
            //Reads all the details from the data model and displays them in the data entry fields
            txtTeamID.Text = teams.TeamID.ToString();
            txtTeamName.Text = teams.TeamName;
            txtPrimaryContact.Text = teams.PrimaryContact;
            txtContactEmail.Text = teams.ContactEmail;
            txtContactPhone.Text = teams.ContactPhone;
            txtCompetitionPoints.Text = teams.CompetitionPoints.ToString();
            //Sets the save type to an Update save type for when the next save is pressed.
            saveType = SaveMode.UpdateSave; 
        }

        /// <summary>
        /// Event triggered when the save button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //If any of the data entry fields are not correctly filled or contain invalid data the save will not
            //proced and a message box will be issued to the user.
            if (FieldsFilledCorrectly() == false)
            {
                MessageBox.Show("Please ensure all fields are filled correctly");
                return;
            }

            //Reads all the details from the data entry fields and stores them in the data model for saving
            teams.TeamName = txtTeamName.Text;
            teams.PrimaryContact = txtPrimaryContact.Text;
            teams.ContactPhone = txtContactPhone.Text;
            teams.ContactEmail = txtContactEmail.Text;
            teams.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);

            //Checks the currently selected save mode and performs the desired save type (new or update) based upon the seletced option.
            //If neither valid option is selected the method will not perform either save tyope.
            if (saveType == SaveMode.NewSave)
            {
                data.AddTeams(teams);
            }
            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateTeams(teams);
            }
            else 
            {
                return;
            }

            //Clears the entry fields and refreshes the UI after the save.
            ClearDataEntryFields();
            UpdateDataGrid();
        }

        /// <summary>
        /// Checks whether each field in the data entry form has valid data selected/entered and returns fale if any field is blank or containt invalid data.
        /// </summary>
        /// <returns>Whether the current data in the entry fields is valid or not.</returns>
        private bool FieldsFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtTeamName.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtPrimaryContact.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtContactEmail.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtContactPhone.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtCompetitionPoints.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// method to clear all the data entry fields back to blank and resets the SaveMode of the form.
        /// </summary>
        private void ClearDataEntryFields()
        {
            txtTeamID.Text = "";
            txtTeamName.Text = "";
            txtPrimaryContact.Text = "";
            txtContactPhone.Text = "";
            txtContactEmail.Text = "";
            txtCompetitionPoints.Text = "";
            saveType = SaveMode.NewSave;
        }

        /// <summary>
        /// Event triggered when the delete button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvTeams.SelectedIndex < 0)
            {
                MessageBox.Show("No row selected for deletion!");
                return;
            }

            //Displays message box to user asking to confirm deletion of selected entry. This message box stores the selected response from the user.
            MessageBoxResult result = MessageBox.Show("Delete this entry?", "Delete Confirmation", MessageBoxButton.YesNo);
            //If the user response was a yes/confirmation response the delete proceeds.
            if (result == MessageBoxResult.Yes)
            {
                //Retrieves the primary key from the currently slected row
                int id = team_list[dgvTeams.SelectedIndex].TeamID;
                //Passes the desired primary key to the delete method to trigger deletion from database.
                data.DeleteTeams(id);

                //Clears and refreshes UI and confirms the deletion with the user via message box.
                ClearDataEntryFields();
                UpdateDataGrid();
                MessageBox.Show("Record Deleted.");
            }
        }

        /// <summary>
        /// Event triggered when the new button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryFields();
        }
    }
}
