#region Imports
using Data_Management;
using Data_Management.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
#endregion

namespace UI
{
    public partial class winGamesPlayed : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<GamesPlayedView> games_played_list = new List<GamesPlayedView>();
        List<Teams> teams_list = new List<Teams>();
        GamesPlayed games_played = new GamesPlayed();
        SaveMode saveType = SaveMode.NewSave;

        public winGamesPlayed()
        {
            InitializeComponent();
            UpdateDataGrid();
        }
        #endregion

        #region Functions
        private void dgvGamesPlayed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvGamesPlayed.SelectedIndex < 0)
            {
                return;
            }

            int id = games_played_list[dgvGamesPlayed.SelectedIndex].GamesPlayedID;
            games_played = data.GetGamesPlayedById(id);
            txtGamesPlayedID.Text = games_played.GamesPlayedID.ToString();
            txtGameName.Text = games_played.GameName;
            txtGameType.Text = games_played.GameType;
            saveType = SaveMode.UpdateSave;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvGamesPlayed.SelectedIndex < 0)
            {
                MessageBox.Show("No row selected for deletion!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Delete this entry?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                int id = games_played_list[dgvGamesPlayed.SelectedIndex].GamesPlayedID;
                data.DeleteEvents(id);
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
            games_played_list = data.GetGamesPlayed();
            dgvGamesPlayed.ItemsSource = games_played_list;
            dgvGamesPlayed.Items.Refresh();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsFilledCorrectly() == false)
            {
                MessageBox.Show("Please ensure all fields are filled correctly");
                return;
            }

            games_played.GameName = txtGameName.Text;
            games_played.GameType = txtGameType.Text;

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

            UpdateDataGrid();
            ClearDataEntryFields();
        }

        private void ClearDataEntryFields()
        {
            txtGamesPlayedID.Text = "";
            txtGameName.Text = "";
            txtGameType.Text = "";

            saveType = SaveMode.NewSave;
        }

        private bool FieldsFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtGameName.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtGameType.Text))
            {
                return false;
            }

            return true;
        }
        #endregion

    }
}
