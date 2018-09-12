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
        int NormalItemTotal;
        int SchedItemTotal;

        public MainWindow()
        {
            InitializeComponent();
            container = WorkItemWrapper;
            InitTimer();            
            checkForExistingItems();
            moveExe();
            var x = GetResourceNames();
        }

        private void checkForExistingItems()
        {
            StringBuilder location = new StringBuilder();
            location.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString()).Append("\\EZ ORGANIZER\\EZODEFAULT.ezo");

            //if APPDATA has EZODEFAULT.ezo
            if (fileio.File.Exists(location.ToString()))
            {
                //import that default file.
                importEZOFile(location.ToString());
            }
            else
            {
                //add one empty work item
                addWorkItem(); //this method also include methods to export default EZO file to APPDATA                
            }
        }

        //Every combobox change and item path change, and item add and remove, they will call this method
        public void writeEZODefault()
        {
            StringBuilder location = new StringBuilder();
            location.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString()).Append("\\EZ ORGANIZER\\EZODEFAULT.ezo");

            writeEZOFile(location.ToString());
        }

        //this will only work with EXE that is not generated for debugging and while running it with Visual Studio because there will be DLL locking and other issues.
        //copy the EXE somewhere else to try.
        public void moveExe()
        {
            string me = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string destination = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            fileio.FileInfo aboutme = new fileio.FileInfo(me);
            StringBuilder fullPathInDestination = new StringBuilder().Append(destination).Append("\\").Append(aboutme.Name);

            
            if (aboutme.DirectoryName == destination)
            {
                //the file is run from startup folder
            }
            else
            {
                //if STARTUP Folder don't have the EZO exe file, ask to copy the exe to startup. (in order to get the EZO run when startup)
                //if STARTUP has it, doesn't matter where is the exe located, because it is loading the EZO default file anyway.
                //this might change when EZO has different format of EZO default when version number increases.
                if (!fileio.File.Exists(fullPathInDestination.ToString()))
                {
                    if (MessageBox.Show("Do you want to put this program into Startup folder" + Environment.NewLine + Environment.NewLine +
                    "(" + destination + ")" + Environment.NewLine + Environment.NewLine +
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
            List<Tuple<ProcessStartInfo,String>> matchedItem = new List<Tuple<ProcessStartInfo,String>>();
            foreach (WorkItemControl x in container.Children)
            {
                var sched = x.schedule;
                var CB = x.dayScheduleCB.SelectedIndex;
                string state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString();
                string type = ((ComboBoxItem)(((WorkItemControl)x).typeCB).SelectedItem).Content.ToString();


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
                            matchedItem.Add(new Tuple<ProcessStartInfo, String> (theProcess, type));

                        }
                    }
                    else
                    {
                        // set to seconds level so that item will only run once when the second is sharp 0.
                        if (sched.DayOfWeek == now.DayOfWeek && sched.Hour == now.Hour && sched.Minute == now.Minute && sched.Second == now.Second)
                        {
                            matchedItem.Add(new Tuple<ProcessStartInfo, String>(theProcess, type));
                        }
                    }
                }                
            }

            if (matchedItem.Count > 0)
            {
                foreach (Tuple<ProcessStartInfo, String> item in matchedItem)
                {
                    //item 1 is task path, item 2 is the type
                    runWorkItem(item.Item1, item.Item2);
                }
            }

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
            countItems();
        }

        public void countItems()
        {
            int sched = 0;
            int normal = 0;

            //read all items
            foreach (var x in container.Children)
            {
                string target = ((WorkItemControl)x).ItemPath.Text;
                string scheduleCB = ((ComboBoxItem)(((WorkItemControl)x).dayScheduleCB).SelectedItem).Content.ToString();
                if (scheduleCB.ToLower() != "no schedule")
                {
                    sched++;
                }
                else
                {
                    normal++;
                }
            }

            countSchedItem.Text = sched.ToString();
            countNormalItem.Text = normal.ToString();
        }
   

        public void addWorkItem()
        {   
            WorkItemControl uc = new EZInnocathon.WorkItemControl();            
            container.Children.Add(uc);

            writeEZODefault();
        }

        private void runAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(var x in container.Children)
            {
                string target = ((WorkItemControl)x).ItemPath.Text;
                string state = ((ComboBoxItem)(((WorkItemControl)x).windowStateCB).SelectedItem).Content.ToString();
                string type = ((ComboBoxItem)(((WorkItemControl)x).typeCB).SelectedItem).Content.ToString();

                ProcessStartInfo theProcess = new ProcessStartInfo(target);

                if (type != "Message")
                {
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
                }

                runWorkItem(theProcess, type);
            }
        }

        public void runWorkItem(ProcessStartInfo target, string type)
        {            
            try
            {
                //this function is used by run all work item.
                //which we don't have to run message type item along
                //run only items that are not Message
                if (type != "Message")
                {
                    System.Diagnostics.Process.Start(target);
                }
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
                writeEZOFile(saveFileDialog.FileName);
            }   
        }

        private void writeEZOFile(string fileNameAndLocation)
        {
            StringBuilder exportFileContent = new StringBuilder();
            foreach (var x in container.Children)
            {
                string daySchedule = "-", hour = "-", minute = "-", ampm = "-";
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
            System.IO.Directory.CreateDirectory(fileio.Path.GetDirectoryName(fileNameAndLocation));
            fileio.File.WriteAllText(fileNameAndLocation, exportFileContent.ToString());
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

        private void importEZOFile(string fileAndLocation)
        {
            try
            {
                if (fileAndLocation.Substring(fileAndLocation.Length - 4).ToUpper() == ".EZO")
                {
                    string readContents;
                    using (fileio.StreamReader streamReader = new fileio.StreamReader(fileAndLocation))
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
                    countItems();
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

        private void importActionButton_Click(object sender, RoutedEventArgs e)
        {
            importEZOFile(ImportTextBox.Text);
        }
    }

}
