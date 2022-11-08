#region Imports
using Data_Management;
using Data_Management.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            SetupTeamComboBox();
        }
        #endregion

        #region Functions
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            Results result = new Results();

            result.Result = txtResults.Text;
            result.FKTeamID = (int)cboTeam.SelectedValue;
            result.FKGamesPlayedID = (int)cboGameName.SelectedValue;
            result.FKEventsID = (int)cboEventName.SelectedValue;
            result.FKTeamID_Opposing = (int)cboOpposingTeam.SelectedValue;


            if (data.ResultsTransaction(result) == false)
            {
                MessageBox.Show("Oops, Something went wrong! \n Please Try again");
                return;
            }

            if (AreFieldsSelectedCorrectly() == false)
            {
                return;
            }

            DialogResult = true;
            Close();
        }

        private void SetupTeamComboBox()
        {
            teams_list = data.GetTeams();
            cboTeam.ItemsSource = teams_list;
            cboTeam.DisplayMemberPath = "TeamName";
            cboTeam.SelectedValuePath = "TeamID";

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
    }
}