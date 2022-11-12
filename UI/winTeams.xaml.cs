#region Imports
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Data_Management;
using Data_Management.Models;
#endregion

namespace UI
{
    public partial class winTeams : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<Teams> team_list = new List<Teams>();
        Teams teams = new Teams();
      
        SaveMode saveType = SaveMode.NewSave;

        public winTeams()
        {
            InitializeComponent();
            UpdateDataGrid();  
        }
        #endregion

        #region Functions
        private void UpdateDataGrid()
        {
           
            team_list = data.GetTeams();
            dgvTeams.ItemsSource = team_list;
            dgvTeams.Items.Refresh();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch
            {
                return;
            }
        }
 
        private void DgvTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            if (dgvTeams.SelectedIndex < 0)
            {
                return;
            }
          
            int id = team_list[dgvTeams.SelectedIndex].TeamID;
         
            teams = data.GetTeamsById(id);
            txtTeamID.Text = teams.TeamID.ToString();
            txtTeamName.Text = teams.TeamName;
            txtPrimaryContact.Text = teams.PrimaryContact;
            txtContactEmail.Text = teams.ContactEmail;
            txtContactPhone.Text = teams.ContactPhone;
            txtCompetitionPoints.Text = teams.CompetitionPoints.ToString();
           
            saveType = SaveMode.UpdateSave; 
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
           
            if (FieldsFilledCorrectly() == false)
            {
                MessageBox.Show("Please ensure all fields are filled correctly");
                return;
            }

            teams.TeamName = txtTeamName.Text;
            teams.PrimaryContact = txtPrimaryContact.Text;
            teams.ContactPhone = txtContactPhone.Text;
            teams.ContactEmail = txtContactEmail.Text;
            teams.CompetitionPoints = int.Parse(txtCompetitionPoints.Text);

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

            ClearDataEntryFields();
            UpdateDataGrid();
        }

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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTeams.SelectedIndex < 0)
            {
                MessageBox.Show("No row selected for deletion!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Delete this entry?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {

                int id = team_list[dgvTeams.SelectedIndex].TeamID;

                data.DeleteTeams(id);
                ClearDataEntryFields();
                UpdateDataGrid();
                MessageBox.Show("Record Deleted.");
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryFields();
        }
        #endregion
    }
}
