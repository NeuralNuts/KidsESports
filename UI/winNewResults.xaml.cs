#region Imports
using Data_Management;
using Data_Management.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
#endregion

namespace UI
{
    public partial class winNewResults : Window
    {
        #region Var
        List<Teams> teams_list = new List<Teams>();
        List<GamesPlayedView> games_list = new List<GamesPlayedView>();
        List<EventView> event_list = new List<EventView>();
        List<ResultView> result_list = new List<ResultView>();
        Results result = new Results(0,0,0,0);
        DataAccess data = new DataAccess();
        SaveMode saveType = SaveMode.NewSave;

        public winNewResults()
        {
            InitializeComponent();
            SetupComboBox();
            SetupComboBox1();
            SetupComboBox2();
            SetupComboBox3();
        }
        #endregion

        #region Functions
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool win = cboResult.SelectedItem.Equals(Win);

            result.Result = cboResult.SelectedValue as string;
            result.FKTeamID = (int)cboTeam.SelectedValue;
            result.FKGamesPlayedID = (int)cboGameName.SelectedValue;
            result.FKEventID = (int)cboEventName.SelectedValue;
            result.FKTeamID_Opposing = (int)cboOpposingTeam.SelectedValue;

            if (saveType == SaveMode.NewSave)
            {
                data.AddResults(result);
            }
            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateResults(result);
            }

            if (saveType == SaveMode.NewSave & win == true)
            {
                data.AddResultsWin(result);
            }
            else if (saveType == SaveMode.UpdateSave)
            {
               data.UpdateResults(result);
            }
            else
            {
                return;
            }

            DialogResult = true;
            Close();
        }

        private void SetupComboBox()
        {
            teams_list = data.GetTeams();
            cboTeam.ItemsSource = teams_list;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "TeamID";
        }

        private void SetupComboBox3()
        {
            teams_list = data.GetTeams();
            cboOpposingTeam.ItemsSource = teams_list;
            cboOpposingTeam.DisplayMemberPath = "TeamName";
            cboOpposingTeam.SelectedValuePath = "TeamID";
        }

        private void SetupComboBox1()
        {
            games_list = data.GetGamesPlayed();
            cboGameName.ItemsSource = games_list;
            cboGameName.DisplayMemberPath = "GameName";
            cboGameName.SelectedValuePath = "GamesPlayedID";
        }

        private void SetupComboBox2()
        {
            event_list = data.GetEvents();
            cboEventName.ItemsSource = event_list;
            cboEventName.DisplayMemberPath = "EventName";
            cboEventName.SelectedValuePath = "EventID";
        }

        private void cboResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string var = cboResult.SelectedValue as string;
            result.Result = var;
        }
        #endregion
    }
}