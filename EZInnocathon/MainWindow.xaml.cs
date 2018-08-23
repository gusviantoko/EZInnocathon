using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Resources;
using System.Reflection;
using System.Collections;

namespace EZInnocathon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel container;
        DispatcherTimer timer1;
        bool isTriggered = false;
        Stopwatch stopWatch = new Stopwatch();


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
            FileInfo aboutme = new FileInfo(me);

            if (aboutme.DirectoryName == destination)
            {
                //the file is already in startup folder
            }
            else
            {
                if (MessageBox.Show("Do you want to put this program into Startup folder ("+destination+"), so that EZ Organizer can be run on Windows startup?", "EZ Organizer is not set up for startup yet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    File.Copy(me, destination + "\\" + aboutme.Name, true);
                    System.Diagnostics.Process.Start(destination + "\\" + aboutme.Name);
                    System.Diagnostics.Process.Start(destination);
                    this.Close();
                }
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
            var now = DateTime.Now;
            
            //if isTriggered is true, ignore the scheduling for next 1 minute, so we dont fire the same app
            if (!isTriggered)
            {
                GetAndRunScheduledItem(now);
            }
            else
            {
                stopWatch.Start();

                if (stopWatch.ElapsedMilliseconds > 60000)
                {
                    stopWatch.Stop();
                    isTriggered = false;
                }
            }
            
        }
        

        private void GetAndRunScheduledItem(DateTime now)
        {
            List<String> matchedItem = new List<string>();
            foreach (WorkItemControl x in container.Children)
            {
                var sched = x.schedule;
                var CB = x.dayScheduleCB.SelectedIndex;

                if (CB == 3 || CB == 4 || CB == 5 || CB == 6 || CB == 7 || CB == 8 || CB == 9 || CB == 10)
                {
                    if (CB == 3)//daily
                    {
                        if (sched.ToString("hh") == now.ToString("hh") && sched.Minute == now.Minute)
                        {
                            matchedItem.Add(x.ItemPath.Text);

                        }
                    }
                    else
                    {
                        if (sched.DayOfWeek == now.DayOfWeek && sched.Hour == now.Hour && sched.Minute == now.Minute)
                        {
                            matchedItem.Add(x.ItemPath.Text);
                            isTriggered = true;

                        }
                    }
                }                
            }

            if (matchedItem.Count > 0)
            {
                foreach (string item in matchedItem)
                {
                    runWorkItem(item);
                }
                isTriggered = true;
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
                //if there's error, let it be. don't tell em
            }
        }

        public void WriteShortcut()
        {
            string path= Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\EZOrganizer.bat";
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

        public void playSoundBottomButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/crit.wav", UriKind.Relative));
            Stream fs = sri.Stream;
            var soundPlayer = new System.Media.SoundPlayer(fs);
            soundPlayer.Play();
        }

        public void playSoundNormalButtons(object sender, RoutedEventArgs e)
        {
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("resource/speech.wav", UriKind.Relative));
            Stream fs = sri.Stream;
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


    }

}
