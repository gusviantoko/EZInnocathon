using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Globalization;
using System.Windows.Resources;
using System.IO;

namespace EZInnocathon
{
    /// <summary>
    /// Interaction logic for WorkItemControl.xaml
    /// </summary>
    public partial class WorkItemControl : UserControl
    {
        public DateTime schedule;                

        public WorkItemControl()
        {
            InitializeComponent();
            hideScheduleControl(dayScheduleCB.SelectedIndex);
            setDefault();
            buildCalendar(dayScheduleCB.SelectedIndex, hourCB.SelectedIndex, minuteCB.SelectedIndex, ampmCB.SelectedIndex);
        }

        private void buildCalendar(int day, int hour, int minute, int ampmcase)
        {
            CultureInfo culture = new CultureInfo("en-US");
            string ampm="AM";

            switch (ampmcase)
            {
                case 0: ampm = "AM"; break;
                case 1: ampm = "AM"; break;
            }

            //just use this for day lol
            string daily = string.Format("1/7/2018 {0}:{1}:00 {2}", hour+1, minute, ampm); //set daily to sunday 00:00AM
            string monday = string.Format("1/1/2018 {0}:{1}:00 {2}", hour+1, minute, ampm);
            string tuesday = string.Format("1/2/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);
            string wednesday = string.Format("1/3/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);
            string thursday = string.Format("1/4/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);
            string friday = string.Format("1/5/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);
            string saturday = string.Format("1/6/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);
            string sunday = string.Format("1/7/2018 {0}:{1}:00 {2}", hour + 1, minute, ampm);

            switch (day)
            {
                case 3: schedule = Convert.ToDateTime(daily, culture); break;
                case 4: schedule = Convert.ToDateTime(monday, culture); break;
                case 5: schedule = Convert.ToDateTime(tuesday, culture); break;
                case 6: schedule = Convert.ToDateTime(wednesday, culture); break;
                case 7: schedule = Convert.ToDateTime(thursday, culture); break;
                case 8: schedule = Convert.ToDateTime(friday, culture); break;
                case 9: schedule = Convert.ToDateTime(saturday, culture); break;
                case 10: schedule = Convert.ToDateTime(sunday, culture); break;
            }            
        }

        public void setDefault()
        {
            schedule = DateTime.MinValue;

            ampmCB.SelectedIndex = 0;
            hourCB.SelectedIndex = 8;
            minuteCB.SelectedIndex = 0;

            monitorCB.SelectedIndex = 0;
            dayScheduleCB.SelectedIndex = 0;

            windowStateCB.SelectedIndex = 0;
            typeCB.SelectedIndex = 0;        
        }

        public void removeButton_Click(object sender, RoutedEventArgs e)
        {        
            ((Panel)this.Parent).Children.Remove(this);
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    int countItem;
                    int.TryParse((window as MainWindow).countNormalItem.Text, out countItem);

                    (window as MainWindow).countNormalItem.Text = (countItem - 1).ToString();
                }
            }
            
        }

        public void hideScheduleControl(int daySchedule)
        {
            if (daySchedule==0 || daySchedule==1)
            {
                ampmCB.Visibility = Visibility.Hidden;
                hourCB.Visibility = Visibility.Hidden;
                minuteCB.Visibility = Visibility.Hidden;
            }
            else
            {
                ampmCB.Visibility = Visibility.Visible;
                hourCB.Visibility = Visibility.Visible;
                minuteCB.Visibility = Visibility.Visible;
            }
        }
        private void typeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemPath.Text = "";
            var selectedValue = ((ComboBoxItem)typeCB.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case null: break;
                case "Website":
                    this.browseButton.IsEnabled = false;
                    this.browseButton.Opacity = 0.5;
                    break;
                default:
                    this.browseButton.IsEnabled = true;
                    this.browseButton.Opacity = 1.0;
                    break;
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            string target = this.ItemPath.Text.ToString();
            runWorkItem(target);
        }

        public void runWorkItem(string target)
        {
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception w)
            {
                MessageBox.Show(w.Message);
            }
        }

        private void windowStateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dayScheduleCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb.SelectedIndex == 0 || cb.SelectedIndex == 1 || cb.SelectedIndex == 2)
            {
                if (cb.SelectedIndex == 2) cb.SelectedIndex = 1;

                ampmCB.Visibility = Visibility.Hidden;
                hourCB.Visibility = Visibility.Hidden;
                minuteCB.Visibility = Visibility.Hidden;
            }
            else
            {   
                ampmCB.Visibility = Visibility.Visible;
                hourCB.Visibility = Visibility.Visible;
                minuteCB.Visibility = Visibility.Visible;
            }

            buildCalendar(dayScheduleCB.SelectedIndex, hourCB.SelectedIndex, minuteCB.SelectedIndex, ampmCB.SelectedIndex);


        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedValue = ((ComboBoxItem)typeCB.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case null: break;
                case "Folder":
                    OpenFileDialog folderBrowser = new OpenFileDialog();
                    // Set validate names and check file exists to false otherwise windows will
                    // not let you select "EZO"
                    folderBrowser.ValidateNames = false;
                    folderBrowser.CheckFileExists = false;
                    folderBrowser.CheckPathExists = true;
                    // Always default to Folder Selection.
                    folderBrowser.FileName = "EZO";
                    Nullable<bool> resultFolder = folderBrowser.ShowDialog();

                    if (resultFolder == true)
                    {
                        ItemPath.Text = System.IO.Path.GetDirectoryName(folderBrowser.FileName);
                    }
                    break;
                default:
                    OpenFileDialog dlg = new OpenFileDialog();
                    Nullable<bool> result = dlg.ShowDialog();

                    if (result == true)
                    {
                        ItemPath.Text = dlg.FileName;
                    } 
                    break;
            }
            
        }

        private void hourCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buildCalendar(dayScheduleCB.SelectedIndex, hourCB.SelectedIndex, minuteCB.SelectedIndex, ampmCB.SelectedIndex);
        }

        private void minuteCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buildCalendar(dayScheduleCB.SelectedIndex, hourCB.SelectedIndex, minuteCB.SelectedIndex, ampmCB.SelectedIndex);
        }

        private void ampmCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buildCalendar(dayScheduleCB.SelectedIndex, hourCB.SelectedIndex, minuteCB.SelectedIndex, ampmCB.SelectedIndex);
        }

        public void playSoundDeleteButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/low.wav", UriKind.Relative));
            Stream fs = sri.Stream;
            var soundPlayer = new System.Media.SoundPlayer(fs);
            soundPlayer.Play();
        }

        public void playSoundBrowseButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/crit.wav", UriKind.Relative));
            Stream fs = sri.Stream;
            var soundPlayer = new System.Media.SoundPlayer(fs);
            soundPlayer.Play();
        }


    }
}
