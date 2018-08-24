using System;
using System.Collections.Generic;
using System.Globalization;
using fileio= System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Resources;
using System.Reflection;
using System.Collections;
using Microsoft.Win32;
using System.Text;
using IWshRuntimeLibrary;

namespace EZInnocathon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel container;
        DispatcherTimer timer1;

        public MainWindow()
        {
            InitializeComponent();
            container = WorkItemWrapper;
            InitTimer();
            moveExe();
            //getCal();
            addWorkItem();
            var x = GetResourceNames();
        }

        //this will only work with EXE that is not generated for debugging and while running it with Visual Studio because there will be DLL locking and other issues.
        //copy the EXE somewhere else to try.
        public void moveExe()
        {
            string me = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            fileio.FileInfo aboutme = new fileio.FileInfo(me);

            //TODO: currently: if not run from startup, EZO will ask for copy the exe to startup, close current running EZO, then run the one in startup.
            //it will be ideal that at first time EZO being copy to startup folder, a copy of export file also be created under APPDATA folder for example, then EZO
            //will read the file everytime.
            if (aboutme.DirectoryName == destination)
            {
                //the file is already in startup folder
            }
            else
            {
                if (MessageBox.Show("Do you want to put this program into Startup folder" +Environment.NewLine + Environment.NewLine +
                    "(" +destination+ ")"+ Environment.NewLine + Environment.NewLine +
                    "So that EZ Organizer can be run on Windows startup?" + Environment.NewLine + Environment.NewLine +
                    "(a shortcut will be created in desktop)", "EZ Organizer is not run from Startup folder", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    fileio.File.Copy(me, destination + "\\" + aboutme.Name, true);
                    System.Diagnostics.Process.Start(destination + "\\" + aboutme.Name);
                    CreateShortcut(aboutme.Name);
                    this.Close();
                }
            }
        }

        private void CreateShortcut(string name)
        {            
            try
            {
                name = name.Substring(0, name.Length - 4);
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\"+ name + ".lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
                shortcut.Description = "Shortcut for EZ Organizer in Startup folder";
                shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\"+name+".exe";
                shortcut.Save();
            }
            catch(Exception)
            {

            }
        }

        public void InitTimer()
        {
            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0 , 1); //1 sec
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetAndRunScheduledItem();
        }
        

        private void GetAndRunScheduledItem()
        {
            DateTime now = DateTime.Now;
            List<ProcessStartInfo> matchedItem = new List<ProcessStartInfo>();
            foreach (WorkItemControl x in container.Children)
            {
                var sched = x.schedule;
                var CB = x.dayScheduleCB.SelectedIndex;
                string state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString();


                ProcessStartInfo theProcess = new ProcessStartInfo(x.ItemPath.Text);
                switch (state)
                {
                    case "Default":
                        theProcess.WindowStyle = ProcessWindowStyle.Normal;
                        break;
                    case "Maximized":
                        theProcess.WindowStyle = ProcessWindowStyle.Maximized;
                        break;
                    case "Minimized":
                        theProcess.WindowStyle = ProcessWindowStyle.Minimized;
                        break;
                }

                if (CB == 3 || CB == 4 || CB == 5 || CB == 6 || CB == 7 || CB == 8 || CB == 9 || CB == 10)
                {
                    if (CB == 3)//daily
                    {
                        // set to seconds level so that item will only run once when the second is sharp 0.
                        if (sched.Hour == now.Hour && sched.Minute == now.Minute && sched.Second == now.Second)
                        {
                            matchedItem.Add(theProcess);

                        }
                    }
                    else
                    {
                        // set to seconds level so that item will only run once when the second is sharp 0.
                        if (sched.DayOfWeek == now.DayOfWeek && sched.Hour == now.Hour && sched.Minute == now.Minute && sched.Second == now.Second)
                        {
                            matchedItem.Add(theProcess);
                        }
                    }
                }                
            }

            if (matchedItem.Count > 0)
            {
                foreach (ProcessStartInfo item in matchedItem)
                {
                    runWorkItem(item);
                }
            }

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
                string state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString();


                ProcessStartInfo theProcess = new ProcessStartInfo(target);
                switch (state)
                {
                    case "Default":
                        theProcess.WindowStyle = ProcessWindowStyle.Normal;
                        break;
                    case "Maximized":
                        theProcess.WindowStyle = ProcessWindowStyle.Maximized;
                        break;
                    case "Minimized":
                        theProcess.WindowStyle = ProcessWindowStyle.Minimized;
                        break;
                }

                runWorkItem(theProcess);
            }
        }

        public void runWorkItem(ProcessStartInfo target)
        {            
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (Exception)
            {
                //if there's error, let it be. don't tell em
            }
        }

        public void WriteShortcut()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Run EZO Work Items.bat";
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

                fileio.File.WriteAllText(path, buildShortcut.ToString());
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
                fileio.File.WriteAllText(saveFileDialog.FileName, exportFileContent.ToString());
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
        
        public void playSoundBottomButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/crit.wav", UriKind.Relative));
            fileio.Stream fs = sri.Stream;
            var soundPlayer = new System.Media.SoundPlayer(fs);
            soundPlayer.Play();
        }

        public void playSoundNormalButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/speech.wav", UriKind.Relative));
            fileio.Stream fs = sri.Stream;
            var soundPlayer = new System.Media.SoundPlayer(fs);
            soundPlayer.Play();
        }

        //only used to identify all resource in resources. debugging purpose only.
        public static string[] GetResourceNames()
        {
            var asm = Assembly.GetEntryAssembly();
            string resName = asm.GetName().Name + ".g.resources";
            using (var stream = asm.GetManifestResourceStream(resName))
            using (var reader = new System.Resources.ResourceReader(stream))
            {
                return reader.Cast<DictionaryEntry>().Select(entry => (string)entry.Key).ToArray();
            }
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
                    using (fileio.StreamReader streamReader = new fileio.StreamReader(ImportTextBox.Text))
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
