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
using System.Media;
using Microsoft.Win32;

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
            int countItem;
            int.TryParse(countNormalItem.Text, out countItem);
            countNormalItem.Text = (countItem + 1).ToString();
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
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\EZOrganizer.bat";
            try
            {
                StringBuilder buildShortcut = new StringBuilder();

                buildShortcut.Append("@echo off").Append(Environment.NewLine);

                foreach (var x in container.Children)
                {
                    buildShortcut.Append("start \"\" ");
                    string target = ((WorkItemControl)x).ItemPath.Text;
                    //((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
                    var state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString(); //phew that's long
                    switch (state)
                    {
                        case "Maximized":
                            buildShortcut.Append(" /MAX ");
                            break;
                        case "Minimized":
                            buildShortcut.Append(" /MIN ");
                            break;
                    }
                    buildShortcut.Append("\"").Append(target).Append("\" ").Append(Environment.NewLine);
                    buildShortcut.Append("sleep 1").Append(Environment.NewLine);
                }

                File.WriteAllText(path, buildShortcut.ToString());
                MessageBox.Show("shortcut has been created in " + path);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "EZO file (*.ezo)|*.ezo";
            if (saveFileDialog.ShowDialog() == true)
            {
                StringBuilder exportFileContent = new StringBuilder();
                foreach (var x in container.Children)
                {
                    string daySchedule="-", hour="-", minute="-", ampm = "-";
                    if (exportFileContent.Length > 0) exportFileContent.Append("\n");
                    exportFileContent.Append("|").Append(((ComboBoxItem)((WorkItemControl)x).typeCB.SelectedItem).Content.ToString());
                    exportFileContent.Append("|").Append(((WorkItemControl)x).ItemPath.Text);
                    exportFileContent.Append("|").Append(((ComboBoxItem)((WorkItemControl)x).windowStateCB.SelectedItem).Content.ToString());
                    exportFileContent.Append("|").Append(((ComboBoxItem)((WorkItemControl)x).monitorCB.SelectedItem).Content.ToString());
                    daySchedule = ((ComboBoxItem)((WorkItemControl)x).dayScheduleCB.SelectedItem).Content.ToString();
                    exportFileContent.Append("|").Append(daySchedule);
                    if (daySchedule != "Run at start" && daySchedule != "No schedule")
                    {
                        hour = ((ComboBoxItem)((WorkItemControl)x).hourCB.SelectedItem).Content.ToString();
                        minute = ((ComboBoxItem)((WorkItemControl)x).minuteCB.SelectedItem).Content.ToString();
                        ampm = ((ComboBoxItem)((WorkItemControl)x).ampmCB.SelectedItem).Content.ToString();
                    }
                    exportFileContent.Append("|").Append(hour);
                    exportFileContent.Append("|").Append(minute);
                    exportFileContent.Append("|").Append(ampm);
                }
                File.WriteAllText(saveFileDialog.FileName, exportFileContent.ToString());
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

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                ImportTextBox.Text = dlg.FileName;
            }
        }

        private void importActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ImportTextBox.Text.Substring(ImportTextBox.Text.Length - 4).ToUpper() == ".EZO")
                {
                    string readContents;
                    using (StreamReader streamReader = new StreamReader(ImportTextBox.Text))
                    {
                        readContents = streamReader.ReadToEnd();
                    }

                    string[] contents = readContents.Split('\n');
                    if (contents.Count() > 0) container.Children.Clear();
                    int idx = 0;
                    foreach (var content in contents)
                    {
                        string[] lineContents = content.Split('|');
                        addWorkItem();

                        ((WorkItemControl)container.Children[idx]).typeCB.SelectedValue = lineContents[1];
                        ((WorkItemControl)container.Children[idx]).ItemPath.Text = lineContents[2];
                        ((WorkItemControl)container.Children[idx]).windowStateCB.SelectedValue = lineContents[3];
                        ((WorkItemControl)container.Children[idx]).monitorCB.SelectedValue = lineContents[4];
                        ((WorkItemControl)container.Children[idx]).dayScheduleCB.SelectedValue = lineContents[5];
                        if (lineContents[6] != "-") ((WorkItemControl)container.Children[idx]).hourCB.SelectedValue = lineContents[6];
                        if (lineContents[7] != "-") ((WorkItemControl)container.Children[idx]).minuteCB.SelectedValue = lineContents[7];
                        if (lineContents[8] != "-") ((WorkItemControl)container.Children[idx]).ampmCB.SelectedValue = lineContents[8];

                        idx++;
                    }
                    countNormalItem.Text = (contents.Count()).ToString();
                }
                else
                {
                    MessageBox.Show("Invalid import file - the extension is not .ezo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
