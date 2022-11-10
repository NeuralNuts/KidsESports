#region Imports
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Data_Management.Models;
using Data_Management;
#endregion

namespace UI
{
    public partial class winEvents : Window
    {
        #region Var
        DataAccess data = new DataAccess();
        List<EventView> event_list = new List<EventView>();
        List<Teams> teams_list = new List<Teams>();
        Events events = new Events();
        SaveMode saveType = SaveMode.NewSave;

        public winEvents()
        {
            InitializeComponent();

            UpdateDataGrid();
        }
        #endregion

        #region Functions
        private void UpdateDataGrid()
        {
            event_list = data.GetEvents();
            dgvEvents.ItemsSource = event_list;
            dgvEvents.Items.Refresh();
        }

        private void dgvEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvEvents.SelectedIndex < 0)
            {
                return;
            }
            
            int id = event_list[dgvEvents.SelectedIndex].EventID;
            events = data.GetEventsByID(id);
            txteventID.Text = events.EventID.ToString();
            txtEventName.Text = events.EventName;
            txtEventLocation.Text = events.EventLocation;
            txtEventDate.Text = events.EventDate;
            saveType = SaveMode.UpdateSave; 
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsFilledCorrectly() == false)
            {
                MessageBox.Show("Please ensure all fields are filled correctly");
                return;
            }

            events.EventName = txtEventName.Text;
            events.EventLocation = txtEventLocation.Text;
            events.EventDate = txtEventDate.Text;

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

            UpdateDataGrid();
            ClearDataEntryFields();
        }

        private void ClearDataEntryFields()
        {
            txteventID.Text = "";
            txtEventName.Text = "";
            txtEventLocation.Text = "";
            txtEventDate.Text = "";
            saveType = SaveMode.NewSave;
        }

        private bool FieldsFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtEventName.Text))
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

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryFields();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvEvents.SelectedIndex < 0)
            {
                MessageBox.Show("No row selected for deletion!");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Delete this entry?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int id = event_list[dgvEvents.SelectedIndex].EventID;
                data.DeleteEvents(id);
                ClearDataEntryFields();
                UpdateDataGrid();
                MessageBox.Show("Record Deleted.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
