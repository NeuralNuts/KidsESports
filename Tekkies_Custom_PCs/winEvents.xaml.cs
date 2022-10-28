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
using Data_Management.Models;
using Data_Management;
using UI;

namespace UI
{
    /// <summary>
    /// Interaction logic for winProducts.xaml
    /// </summary>
    public partial class winEvents : Window
    {
        DataAccess data = new DataAccess();
        List<EventView> event_list = new List<EventView>();
        List<Teams> teams_list = new List<Teams>();
        Events events = new Events();
        //Enumeration used to act as a trigger/selector to specify the current type of save to be used.
        //NOTE: See SaveMode enum for details.
        SaveMode saveType = SaveMode.NewSave;

        public winEvents()
        {
            InitializeComponent();

            UpdateDataGrid();
            SetupComboBox();
        }

        /// <summary>
        /// Sets up the combo box to display all the available Categories from the database. 
        /// </summary>
        private void SetupComboBox()
        {
            //Retrieves all Category records from the databse and stores them in the list.
            teams_list = data.GetTeams();
            //Sets the data source of the combo box to be based uopn the provided list.
            cboTeamName.ItemsSource = teams_list;
            //Specifies which property from each Category record is to be displayed in the combo box
            cboTeamName.DisplayMemberPath = "TeamName";
            //Specifies the property of each record which is to be set as the value for each option in the combo box.
            cboTeamName.SelectedValuePath = "TeamID";
        }

        /// <summary>
        /// Sets up the data grid to display all the records of Products list.
        /// </summary>
        private void UpdateDataGrid()
        {
            //Retrieves all Product records from the databse and stores them in the list.
            event_list = data.GetEvents();
            //Sets the data source of the data grid to be based uopn the provided list.
            dgvEvents.ItemsSource = event_list;
            //Refreshes and updates the display of the data grid.
            dgvEvents.Items.Refresh();
        }

        /// <summary>
        /// Event triggered when the selected record of the data grid is changed by either selecting or unselecting a row. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void dgvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvEvents.SelectedIndex < 0)
            {
                return;
            }
            
            //Retrieves the primary key from the currently slected row
            int id = event_list[dgvEvents.SelectedIndex].EventID;
            //Retrieves the desired record matching the selected id.
            events = data.GetEventsByID(id);

            //Reads all the details from the data model and displays them in the data entry fields
            txteventID.Text = events.EventID.ToString();
            txtEventName.Text = events.EventName;
            //Sets the display of the combo box to match the value of the Category Foreign key field.
            cboTeamName.SelectedValue = events.FKTeamID;
            txtEventLocation.Text = events.EventLocation;
            txtEventDate.Text = events.EventDate;
            

            //Sets the save type to an Update save type for when the next save is pressed.
            saveType = SaveMode.UpdateSave; 
        }

        /// <summary>
        /// Event triggered when the save button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //If any of the data entry fields are not correctly filled or contain invalid data the save will not
            //proced and a message box will be issued to the user.
            if (FieldsFilledCorrectly() == false)
            {
                MessageBox.Show("Please ensure all fields are filled correctly");
                return;
            }

            //Reads all the details from the data entry fields and stores them in the data model for saving
            events.EventName = txtEventName.Text;
            //Retrieves the value(primary key) from the selected entry in the combo box.
            events.FKTeamID = (int)cboTeamName.SelectedValue;
            events.EventLocation = txtEventLocation.Text;
            events.EventDate = txtEventDate.Text;

            //Checks the currently selected save mode and performs the desired save type (new or update) based upon the seletced option.
            //If neither valid option is selected the method will not perform either save tyope.
            if (saveType == SaveMode.NewSave)
            {
                data.AddEvents(events);
            }
            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateEvents(events);
            }
            else 
            {
                return;
            }

            //Clears the entry fields and refreshes the UI after the save.
            UpdateDataGrid();
            ClearDataEntryFields();

        }

        /// <summary>
        /// method to clear all the data entry fields back to blank and resets the SaveMode of the form.
        /// </summary>
        private void ClearDataEntryFields()
        {
            txteventID.Text = "";
            txtEventName.Text = "";
            txtEventLocation.Text = "";
            txtEventDate.Text = "";
            cboTeamName.SelectedIndex = -1;

            saveType = SaveMode.NewSave;
        }

        /// <summary>
        /// Checks whether each field in the data entry form has valid data selected/entered and returns fale if any field is blank or containt invalid data.
        /// </summary>
        /// <returns>Whether the current data in the entry fields is valid or not.</returns>
        private bool FieldsFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtEventName.Text))
            {
                return false;
            }
            if (cboTeamName.SelectedIndex < 0)
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtEventDate.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtEventLocation.Text))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Event triggered when the new button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryFields();
        }

        /// <summary>
        /// Event triggered when the delete button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvEvents.SelectedIndex < 0)
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
                int id = event_list[dgvEvents.SelectedIndex].EventID;
                //Passes the desired primary key to the delete method to trigger deletion from database.
                data.DeleteEvents(id);

                //Clears and refreshes UI and confirms the deletion with the user via message box.
                ClearDataEntryFields();
                UpdateDataGrid();
                MessageBox.Show("Record Deleted.");
            }
        }

        /// <summary>
        /// Event triggered when the close button is pressed. 
        /// </summary>
        /// <param name="sender">The component triggering the the event.</param>
        /// <param name="e">Details fo the event passed by the calling component.</param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
