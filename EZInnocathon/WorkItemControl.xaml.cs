using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EZInnocathon
{
    /// <summary>
    /// Interaction logic for WorkItemControl.xaml
    /// </summary>
    public partial class WorkItemControl : UserControl
    {
        public WorkItemControl()
        {
            InitializeComponent();
            hideScheduleControl(dayScheduleCB.SelectedIndex);
            setDefault();
        }

        public void setDefault()
        {
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
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedValue = ((ComboBoxItem)typeCB.SelectedItem).Content.ToString();
            switch (selectedValue)
            {
                case null: break;
                case "Website":
                    this.browseButton.IsEnabled = false;
                    this.browseButton.Opacity = 0.5;
                    break;
                case "Folder":
                    this.browseButton.IsEnabled = true;
                    this.browseButton.Opacity = 1.0;
                    break;
                case "File":
                    this.browseButton.IsEnabled = true;
                    this.browseButton.Opacity = 1.0;
                    break;
                case "Program":
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
            

        }
    }
}
