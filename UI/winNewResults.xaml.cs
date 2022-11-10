#region Imports
using Data_Management;
using Data_Management.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UI;
#endregion

namespace UI
{
    public partial class winNewResults : Window
    {
        #region Var
        List<Teams> teams_list = new List<Teams>();
        List<Teams> opposing_teams_list = new List<Teams>();
        List<GamesPlayedView> games_list = new List<GamesPlayedView>();
        List<EventView> event_list = new List<EventView>();
        List<ResultView> result_list = new List<ResultView>();
        DataAccess data = new DataAccess();
        SaveMode saveType = SaveMode.NewSave;

        public winNewResults()
        {
            InitializeComponent();
            SetupComboBox1();
            SetupComboBox2();
            SetupComboBox4();
            SetupComboBox3();
        }
        #endregion

        #region Functions
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Results result = new Results();
            result.FKTeamID = (int)cboTeam.SelectedValue;
            result.FKGamesPlayedID = (int)cboGameName.SelectedValue;
            result.FKEventsID = (int)cboEventName.SelectedValue;
            result.FKTeamID_Opposing = (int)cboOpposingTeam.SelectedValue;

            if ((bool)btnTeam.IsChecked == true)
            {
                result.Result = txtResults.Text = "WIN";
                data.WinTransaction(result);
                RefreshDataTable();
            }
            else
            {
                data.LossTransaction(result);
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
            if (AreFieldsSelectedCorrectly() == false)
            {
                return;
            }

            DialogResult = true;
            Close();
        }

        private void RefreshDataTable()
        {
            winResults win_results = new winResults();

            result_list = data.GetAllResults();
            win_results.dgvResults.ItemsSource = result_list;
            win_results.dgvResults.Items.Refresh();
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
        #endregion

        private void btnTeam_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}