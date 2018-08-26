﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace IceChips
{
    /// <summary>
    /// Interaction logic for PTTSettingsWindow.xaml
    /// </summary>
    public partial class PTTSettingsWindow : Window
    {
        private MainWindow mainwindowreference;
        public ObservableCollection<BindingBoy> BindingBoys = new ObservableCollection<BindingBoy>();
        public PTTSettingsWindow(MainWindow mainwindowreference)
        {
            InitializeComponent();
            this.mainwindowreference = mainwindowreference;
            this.Loaded += OnLoad;
        }
        private void OnLoad(object o1, object o2)
        {
            populatewindowlist();
            LoadPTTFromFile();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void populatewindowlist()
        {
            List<string> windownames = new List<string>();

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    windownames.Add(process.ProcessName);
                }
            }

            WindowNameComboBox.ItemsSource = windownames;
        }
        public class BindingBoy
        {
            public string WindowName { get; set; }
            public string Binding { get; set; }
        }
        public void ButtonClickBoy(object sender, object uselessboy)
        {
            Button b = (Button)sender;
            switch (b.Name)
            {
                case "KeyGrabber":
                    mainwindowreference.sm_kp = MainWindow._sm_kp.GET_BINDING;
                    break;
                case "WindowRefreshButton":
                    populatewindowlist();
                    break;
                case "AddBindingButton":

                    BindingBoys.Add(new BindingBoy()
                    {
                        WindowName=(string)(WindowNameComboBox.SelectedValue),
                        Binding=KeyBindingTextBox.Text
                    });
                    

                    BindingDataGrid.ItemsSource = BindingBoys;
                    break;
                case "SaveButton":
                    SavePTTToFile();
                    break;
                case "CancelButton":
                    LoadPTTFromFile();
                    break;
            }
        }
        private const string PTTSavedSettingsFileName = "pttsavedsettings.meme";
        private void SavePTTToFile()
        {
            List<string> lines = new List<string>();
            foreach (var x in BindingBoys)
            {
                lines.Add(x.WindowName);
                lines.Add(x.Binding);
            }
            File.WriteAllLines(PTTSavedSettingsFileName, lines.ToArray());
        }
        private void LoadPTTFromFile()
        {
            try
            {
                BindingBoys = new ObservableCollection<BindingBoy>();
                string[] lines = File.ReadAllLines(PTTSavedSettingsFileName);
                int i = -1;
                while (i < lines.Length - 1)
                {
                    if (lines[i + 1] == "")
                        break;
                    BindingBoy boi = new BindingBoy()
                    {
                        WindowName = lines[++i],
                        Binding = lines[++i]
                    };
                    BindingBoys.Add(boi);
                }

                BindingDataGrid.ItemsSource = BindingBoys;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "\r\n" + e.StackTrace);
            }
        }
        public void OnWindowClosing(object o1, CancelEventArgs cea)
        {
            cea.Cancel = true;
            this.Hide();
        }
        
    }
}
