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
    public partial class winResults : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<ResultView> result_list = new List<ResultView>();
        List<Teams> teams_list = new List<Teams>();
        List<Teams> opposing_teams_list = new List<Teams>();
        List<GamesPlayedView> games_list = new List<GamesPlayedView>();
        List<EventView> event_list = new List<EventView>();
        Results result = new Results();
        ResultView result_view = new ResultView();
        SaveMode saveType = SaveMode.NewSave;

        public winResults()
        {
            InitializeComponent();
            RefreshDataTable();
            SetupComboBox1();
            SetupComboBox2();
            SetupComboBox3();
            SetupComboBox4();
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
            txtResults.Text = "";
            cboEventName.SelectedIndex = -1;
            cboGameName.SelectedIndex = -1;
            cboTeam.SelectedIndex = -1;
            cboOpposingTeam.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearDataEntryFields()
        {
            txtResults.Text = "";
            cboEventName.SelectedIndex = -1;
            cboGameName.SelectedIndex = -1;
            cboTeam.SelectedIndex = -1;
            cboOpposingTeam.SelectedIndex = -1;
            saveType = SaveMode.NewSave;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Results result = new Results();
            result.FKTeamID = (int)cboTeam.SelectedValue;
            result.FKGamesPlayedID = (int)cboGameName.SelectedValue;
            result.FKEventsID = (int)cboEventName.SelectedValue;
            result.FKTeamID_Opposing = (int)cboOpposingTeam.SelectedValue;

            //Saves based on the raido button//
            if (saveType == SaveMode.NewSave)
            {
                if ((bool)btnTeam.IsChecked == true)
                {
                    result.Result = txtResults.Text = "WIN";
                    data.WinTransaction(result);
                    RefreshDataTable();
                }
                if ((bool)btnOpposing.IsChecked == true)
                {
                    result.Result = txtResults.Text = "LOSS";
                    data.LossTransaction(result);
                    RefreshDataTable();
                }
                if ((bool)btnDraw.IsChecked == true)
                {
                    result.Result = txtResults.Text = "DRAW";
                    data.DrawTransaction(result);
                    RefreshDataTable();
                }
            }
            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateResults(result);

                if ((bool)btnTeam.IsChecked == true)
                {
                    result.Result = txtResults.Text = "WIN";
                    data.WinTransaction(result);
                    RefreshDataTable();
                }
                if ((bool)btnOpposing.IsChecked == true)
                {
                    result.Result = txtResults.Text = "LOSS";
                    data.LossTransaction(result);
                    RefreshDataTable();
                }
                if ((bool)btnDraw.IsChecked == true)
                {
                    result.Result = txtResults.Text = "DRAW";
                    data.DrawTransaction(result);
                    RefreshDataTable();
                }
            }
            else if (AreFieldsSelectedCorrectly() == false)
            {
                return;
            }
            else
            {
                return;
            }

            RefreshDataTable();
            ClearDataEntryFields();
        }

        private void SetupComboBox1()
        {
            games_list = data.GetGamesPlayed();
            cboGameName.ItemsSource = games_list;
            cboGameName.DisplayMemberPath = "GameName";
            cboGameName.SelectedValuePath = "GamesPlayedID";
        }

        private void SetupComboBox3()
        {
            teams_list = data.GetTeams();
            cboTeam.ItemsSource = teams_list;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "TeamID";
        }

        private void SetupComboBox4()
        {
            opposing_teams_list = data.GetTeams();
            cboOpposingTeam.ItemsSource = opposing_teams_list;
            cboOpposingTeam.DisplayMemberPath = "TeamName";
            cboOpposingTeam.SelectedValuePath = "TeamID";
        }


        private void SetupComboBox2()
        {
            event_list = data.GetEvents();
            cboEventName.ItemsSource = event_list;
            cboEventName.DisplayMemberPath = "EventName";
            cboEventName.SelectedValuePath = "EventID";
        }

        private bool AreFieldsSelectedCorrectly()
        {
            if (cboTeam.SelectedIndex == -1)
            {
                return false;
            }
            if (cboOpposingTeam.SelectedIndex == -1)
            {
                return false;
            }
            if (txtResults.Text == null)
            {
                return false;
            }
            if (cboEventName.SelectedIndex == -1)
            {
                return false;
            }
            if (cboGameName.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }

        private void dgvResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvResults.SelectedIndex < 0)
            {
                return;
            }

            int id = result_list[dgvResults.SelectedIndex].ResultsID;

            result = data.GetResultsById(id);
            txtResultID.Text = result.ResultsID.ToString();
            txtResults.Text = result.Result;
            cboEventName.SelectedIndex = result.FKEventsID;
            cboGameName.SelectedIndex = result.FKGamesPlayedID;
            cboTeam.SelectedIndex = result.FKTeamID;
            cboOpposingTeam.SelectedIndex = result.FKTeamID_Opposing;

            saveType = SaveMode.UpdateSave;
        }
    }
    #endregion
}
