using Data_Management;
using Data_Management.Models;
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
    /// Interaction logic for winGamesPlayed.xaml
    /// </summary>
    public partial class winGamesPlayed : Window
    {
        DataAccess data = new DataAccess();
        List<GamesPlayedView> games_played_list = new List<GamesPlayedView>();
        List<Teams> teams_list = new List<Teams>();
        GamesPlayed games_played = new GamesPlayed();
        //Enumeration used to act as a trigger/selector to specify the current type of save to be used.
        //NOTE: See SaveMode enum for details.
        SaveMode saveType = SaveMode.NewSave;

        public winGamesPlayed()
        {
            InitializeComponent();
            SetupComboBox();
            UpdateDataGrid();
        }

        private void dgvGamesPlayed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvGamesPlayed.SelectedIndex < 0)
            {
                return;
            }

            //Retrieves the primary key from the currently slected row
            int id = games_played_list[dgvGamesPlayed.SelectedIndex].GamesPlayedID;
            //Retrieves the desired record matching the selected id.
            games_played = data.GetGamesPlayedById(id);

            //Reads all the details from the data model and displays them in the data entry fields
            txtGamesPlayedID.Text = games_played.GamesPlayedID.ToString();
            txtGameName.Text = games_played.GameName;
            //Sets the display of the combo box to match the value of the Category Foreign key field.
            cboTeamName.SelectedValue = games_played.FKTeamID;
            txtGameType.Text = games_played.GameType;

            //Sets the save type to an Update save type for when the next save is pressed.
            saveType = SaveMode.UpdateSave;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Checks whether the data grid currently has a valid row selected. If none is selected it will return a value of -1 
            if (dgvGamesPlayed.SelectedIndex < 0)
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
                int id = games_played_list[dgvGamesPlayed.SelectedIndex].GamesPlayedID;
                //Passes the desired primary key to the delete method to trigger deletion from database.
                data.DeleteEvents(id);

                //Clears and refreshes UI and confirms the deletion with the user via message box.
                ClearDataEntryFields();
                UpdateDataGrid();
                MessageBox.Show("Record Deleted.");
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryFields();
        }

        private void UpdateDataGrid()
        {
            //Retrieves all Product records from the databse and stores them in the list.
            games_played_list = data.GetGamesPlayed();
            //Sets the data source of the data grid to be based uopn the provided list.
            dgvGamesPlayed.ItemsSource = games_played_list;
            //Refreshes and updates the display of the data grid.
            dgvGamesPlayed.Items.Refresh();
        }

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
            games_played.GameName = txtGameName.Text;
            //Retrieves the value(primary key) from the selected entry in the combo box.
            games_played.FKTeamID = (int)cboTeamName.SelectedValue;
            games_played.GameType = txtGameType.Text;

            //Checks the currently selected save mode and performs the desired save type (new or update) based upon the seletced option.
            //If neither valid option is selected the method will not perform either save tyope.
            if (saveType == SaveMode.NewSave)
            {
                data.AddGamesPlayed(games_played);
            }
            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateGamesPlayed(games_played);
            }
            else
            {
                return;
            }

            //Clears the entry fields and refreshes the UI after the save.
            UpdateDataGrid();
            ClearDataEntryFields();
        }

        private void ClearDataEntryFields()
        {
            txtGamesPlayedID.Text = "";
            txtGameName.Text = "";
            txtGameType.Text = "";
            cboTeamName.SelectedIndex = -1;

            saveType = SaveMode.NewSave;
        }

        private bool FieldsFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtGameName.Text))
            {
                return false;
            }
            if (cboTeamName.SelectedIndex < 0)
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtGameType.Text))
            {
                return false;
            }

            return true;
        }
    }
}
