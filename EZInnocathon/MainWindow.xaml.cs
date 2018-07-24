using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel container;

        public MainWindow()
        {
            InitializeComponent();
            container = WorkItemWrapper;
            getCal();
            addWorkItem();            
        }

        private void getCal()
        {
            dayText.Text = DateTime.Today.DayOfWeek.ToString();
            monthText.Text = DateTime.Today.ToString("MMMM", CultureInfo.CreateSpecificCulture("en"));
            dateText.Text = DateTime.Today.Day.ToString();
            
        }
        
        private void CreditButton_Click(object sender, RoutedEventArgs e)
        {
            aboutPanel.Visibility = Visibility.Visible;
            importPanel.Visibility = Visibility.Collapsed;
            mainPanel.Visibility = Visibility.Collapsed;
            helpPanel.Visibility = Visibility.Collapsed;
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            helpPanel.Visibility = Visibility.Visible;
            importPanel.Visibility = Visibility.Collapsed;
            mainPanel.Visibility = Visibility.Collapsed;
            aboutPanel.Visibility = Visibility.Collapsed;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            importPanel.Visibility = Visibility.Visible;
            helpPanel.Visibility = Visibility.Collapsed;
            mainPanel.Visibility = Visibility.Collapsed;
            aboutPanel.Visibility = Visibility.Collapsed;
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {             
            mainPanel.Visibility = Visibility.Visible;
            importPanel.Visibility = Visibility.Collapsed;
            helpPanel.Visibility = Visibility.Collapsed;
            aboutPanel.Visibility = Visibility.Collapsed;
        }

        private void addItemButton_Click(object sender, RoutedEventArgs e)
        {
            addWorkItem();
        }
   

        public void addWorkItem()
        {
            WorkItemControl uc = new EZInnocathon.WorkItemControl();

            container.Children.Add(uc);
        }

        private void runAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var x in container.Children)
            {
                string target = ((WorkItemControl)x).ItemPath.Text;
                runWorkItem(target);
            }
        }

        public void runWorkItem(string target)
        {
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception)
            {
            }
        }

        public void WriteShortcut()
        {
            string path= Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\EZOrganize.bat";
            try
            {
                string buildShortcut;

                buildShortcut = "@echo off" + Environment.NewLine;

                foreach (var x in container.Children)
                {
                    buildShortcut += "start \"\" ";
                    string target = ((WorkItemControl)x).ItemPath.Text;
                    //((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    var state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString(); //phew that's long
                    switch (state)
                    {
                       // case "Maximized": buildShortcut += " /M ";

                    }
                    buildShortcut += "\"" + target + "\" " + Environment.NewLine;

                }

                File.WriteAllText(path, buildShortcut);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void shortcutButton_Click(object sender, RoutedEventArgs e)
        {
            WriteShortcut();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var move = sender as DockPanel;
            var win = Window.GetWindow(move);
            win.DragMove();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void miniButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }


    }
}
