#region Imports
using Data_Management;
using Data_Management.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
#endregion

namespace UI
{
    public partial class winReports : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<string> reportOptions = new List<string> { "Teams By Points", "Results By Event", "Results By Team" };
        List<Teams> fullTeamList;
        List<Teams> filteredTeamList = new List<Teams>();
        List<Teams> TeamDisplayList;
        List<ResultView> fullGamesPlayedList;
        List<ResultView> filteredGamesPlayedList = new List<ResultView>();
        List<ResultView> displayGamesPlayedList;

        public winReports()
        {
            InitializeComponent();
            cboType.ItemsSource = reportOptions;
        }
        #endregion

        #region Functions
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboType.SelectedIndex < 0)
            {
                return;
            }
            else if (cboType.SelectedIndex == 0)
            {
                fullTeamList = data.GetTeams();
                fullTeamList = fullTeamList.OrderBy(s => s.CompetitionPoints).ToList();
                DisplayActiveTeamList(fullTeamList);

            }
            else if (cboType.SelectedIndex == 1)
            {
                fullGamesPlayedList = data.GetAllResults();
                fullGamesPlayedList = fullGamesPlayedList.OrderBy(c => c.EventName).ToList();
                DisplayActiveGamesPlayedList(fullGamesPlayedList);
            }
            else
            {
                fullGamesPlayedList = data.GetAllResults();
                fullGamesPlayedList = fullGamesPlayedList.OrderByDescending(c => c.Team + c.Opposing.Remove(0, 1)).ToList();
                DisplayActiveGamesPlayedList(fullGamesPlayedList);
            }
            txtSearch.Text = "";
        }

        private void DisplayActiveGamesPlayedList(List<ResultView> activeList)
        {
            displayGamesPlayedList = activeList;
            dgvReport.ItemsSource = displayGamesPlayedList;
            dgvReport.Items.Refresh();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (cboType.SelectedIndex < 0)
            {
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Simple Text File(.txt)|*.txt|" +
                                "Comma Separated Values (.csv)|*.csv";
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveDialog.ShowDialog() == true)
            {
                if (cboType.SelectedIndex == 0)
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        foreach (var item in TeamDisplayList)
                        {
                            writer.WriteLine($"{item.TeamID},{item.TeamName},{item.PrimaryContact},{item.ContactEmail},{item.ContactPhone},{item.CompetitionPoints}");
                        }
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        foreach (var item in displayGamesPlayedList)
                        {
                            writer.WriteLine($"{item.ResultsID},{item.Result},{item.Team},{item.Opposing}, {item.EventName}, {item.GameName}");
                        }
                    }
                }
            }

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cboType.SelectedIndex < 0)
            {
                return;
            }

            if (cboType.SelectedIndex == 0)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    DisplayActiveTeamList(fullTeamList);
                    return;
                }

                filteredTeamList = fullTeamList.Where(f => f.TeamName.ToUpper().Contains(txtSearch.Text.ToUpper())
                                                           || f.ContactEmail.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();
                DisplayActiveTeamList(filteredTeamList);
            }

            if (cboType.SelectedIndex > 0)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    DisplayActiveGamesPlayedList(fullGamesPlayedList);
                    return;
                }
                filteredGamesPlayedList = fullGamesPlayedList.Where(p => p.Team.ToUpper().Contains(txtSearch.Text.ToUpper()) ||
                                                              p.Opposing.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();
                DisplayActiveGamesPlayedList(filteredGamesPlayedList);
            }

            if (cboType.SelectedIndex > 0)
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    DisplayActiveGamesPlayedList(fullGamesPlayedList);
                    return;
                }
                filteredGamesPlayedList = fullGamesPlayedList.Where(p => p.EventName.ToUpper().Contains(txtSearch.Text.ToUpper()) ||
                                                              p.Opposing.ToUpper().Contains(txtSearch.Text.ToUpper())).ToList();
                DisplayActiveGamesPlayedList(filteredGamesPlayedList);
            }
        }

        private void DisplayActiveTeamList(List<Teams> activeList)
        {
            TeamDisplayList = activeList;
            dgvReport.ItemsSource = TeamDisplayList;
            dgvReport.Items.Refresh();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
