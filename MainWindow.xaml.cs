using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WeatherAlert_DB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer RefreshUIDelayTimer;
        public MainWindow()
        {
            InitializeComponent();
            InitializeWindowLogic();
        }
        private void InitializeWindowLogic()
        {
            // Perform necessary population of controls and events on launch
            UpdateEventViewUI();
            AddEventsToKeywordCheckBoxs();

            // Handle the Dispatcher Timer
            RefreshUIDelayTimer = new DispatcherTimer();
            RefreshUIDelayTimer.Tick += RefreshUIDelayTimer_Tick;
            RefreshUIDelayTimer.Interval = new TimeSpan(0, 0, 0, 0, 450);
            RefreshUIDelayTimer.Start();
        }
        private void StartRefreshUIDelayTimer()
        {
            // Stop the timer if its already going and then start it
            RefreshUIDelayTimer.Stop();
            RefreshUIDelayTimer.Start();
        }
        private void DatabaseOptions_Button_Click(object sender, RoutedEventArgs e)
        {
            //Show user the DB Options Window
            DatabaseOptions databaseOptions = new DatabaseOptions();
            databaseOptions.Owner = this;
            databaseOptions.ShowDialog();
        }
        /// <summary>
        /// Refresh and Display control data to the user for the entire EventViewer section.
        /// </summary>
        private void UpdateEventViewUI()
        {
            UpdateUIElements.PopulateAllEventViewControls(
                EventView_ListView, EV_EventID_TextBox, EV_DateStart_DatePicker,
                EV_DateEnd_DatePicker, EV_EventType_ComboBox, EV_State_ComboBox,
                 EV_Severity_ComboBox,EV_Keywords_ListBox, Bottom_StatusBar);
        }
        /// <summary>
        /// Refresh and Display control data to the user for the entire GraphView section.
        /// </summary>
        private void UpdateGraphViewUI()
        {
            GraphView_Plot.plt.Clear();  
            UpdateUIElements.UpdateGraphViewUI(GV_Filterby_ComboBox, GV_PieChart_RadioButton, GV_BarGraph_RadioButton, GraphView_Plot);
        }
        private void AddEventsToKeywordCheckBoxs()
        {
            // Ensure the Checkbox's generated can also update the ListView
            foreach (CheckBox checkbox in EV_Keywords_ListBox.Items)
            {
                checkbox.Checked += KeyWordCheckBox_Changed;
                checkbox.Unchecked += KeyWordCheckBox_Changed;
            }
        }
        private void ShowUserFirstTimeHelpWindow()
        {
            // Check if user has ran app before, if not then show them the help window.
            if (!Properties.Settings.Default.UserRanAppBefore)
            {
                InitialHelp_Window initialHelp_Window = new InitialHelp_Window();
                initialHelp_Window.Owner = this;
                initialHelp_Window.Show();

                // set the property so the Help window doesnt show again
                Properties.Settings.Default.UserRanAppBefore = true;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If user attempts to close this MainWindow then close all other currently open windows and exit application.
            foreach (Window windows in this.OwnedWindows)
            {
                windows.Close();
            }
            // Lastly save any settings from the user before closing
            Properties.Settings.Default.Save();
        }

        // -------------------------------------------
        // - Event Section                           -
        // -------------------------------------------

        private void KeyWordCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_EventID_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_DateStart_DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_DateEnd_DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_EventType_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_State_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EV_Severity_ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void EventView_ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Show the user more info if they double click an alert
            ExpandedAlertViewer_Window expandedAlertViewer_Window = new ExpandedAlertViewer_Window((Alert)EventView_ListView.SelectedItem);
            expandedAlertViewer_Window.Owner = this;
            expandedAlertViewer_Window.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowUserFirstTimeHelpWindow();
        }
        private void GV_PieChart_RadioButton_Click(object sender, RoutedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void GV_BarGraph_RadioButton_Click(object sender, RoutedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void GV_Filterby_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StartRefreshUIDelayTimer();
        }
        private void RefreshUIDelayTimer_Tick(object sender, EventArgs e)
        {
            RefreshUIDelayTimer.Stop();
            UpdateEventViewUI();
            UpdateGraphViewUI();
        }
    } 
}
