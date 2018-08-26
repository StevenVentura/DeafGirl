using NAudio.Wave;
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
using SharpTalk;
using System.Threading;
using System.Diagnostics;
using WindowScrape.Static;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using BotPS2;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using MouseKeyboardLibrary;
using System.IO;

namespace IceChips
{
    /*
     * oh ok
would it be possible to make a
floating window
maybe transparent
idk
ya
but thats the gay
graphical stuff
AND
I WANT IT TO SPIN
AND SCREAM
"HEN"
LOOOOOOOO
     * */
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }
        private void OnLoad(object o1, object o2)
        {
            STILL_LOADING_BTW = true;
            List<string> speakerlist = new List<string>();
            for (int n = -1; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                Console.WriteLine(caps.ProductName);
                speakerlist.Add(caps.ProductName);
            }
            SpeakerSelector.ItemsSource = speakerlist;

            
            MicSelector.ItemsSource = speakerlist;



            foreach (var value in Enum.GetValues(typeof(TtsVoice)))
                VoiceSelector.Items.Add(value);
            
            
            LoadPreviousSettingsFromFile();
            if (VoiceSelector.SelectedIndex == -1)
                VoiceSelector.SelectedIndex = 0;
            if (SpeakerSelector.SelectedIndex == -1)
                SpeakerSelector.SelectedIndex = 0;
            if (MicSelector.SelectedIndex == -1)
                MicSelector.SelectedIndex = 0;
            CURRENT_MIC_VALUE = (string)(MicSelector.SelectedValue);
            CURRENT_SPEAKER_VALUE = (string)(SpeakerSelector.SelectedValue);


            this.begin();
        }
        private void w(object o)
        {
            Console.WriteLine(o.ToString());
        }
        private const string filesettingspath = "previoussettings.stebin";
        private bool STILL_LOADING_BTW = true;
        private void SaveSettingsToFile()
        {
            if (STILL_LOADING_BTW) return;
            List<string> lines = new List<string>();
            string n = "\r\n";
            int i = -1;
            lines.Add(VoiceSelector.SelectedValue + "");
            lines.Add(shroud.Value + "");
            lines.Add((LucciHear.IsChecked == true) + "");
            lines.Add((MoonbaseCheckbox.IsChecked == true) + "");
            lines.Add(MicSelector.SelectedValue + "");
            lines.Add(SpeakerSelector.SelectedValue + "");
            lines.Add((OverlayPlaybackCheckbox.IsChecked == true) + "");

            File.WriteAllLines(filesettingspath, lines.ToArray());


        }
        private void LoadPreviousSettingsFromFile()
        {
            try
            {
                string[] lines = File.ReadAllLines(filesettingspath);
                int i = -1;
                VoiceSelector.SelectedValue = Enum.Parse(typeof(TtsVoice), lines[++i]);
                shroud.Value = Double.Parse(lines[++i]);
                LucciHear.IsChecked = bool.Parse(lines[++i]);
                MoonbaseCheckbox.IsChecked = bool.Parse(lines[++i]);
                MicSelector.SelectedValue = lines[++i];
                SpeakerSelector.SelectedValue = lines[++i];
                OverlayPlaybackCheckbox.IsChecked = bool.Parse(lines[++i]);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "\r\n" + e.StackTrace);
            }
            finally
            {
                STILL_LOADING_BTW = false;
            }
        }
        private string memetext(string boring)
        {
            string meme = "";
            //   :nv
            // [:nv] nick[k < 1, 90 >]is[s<1,40>] an[n < 800, 200 >] nibba[r < 900, 400 >]
            //[:nh] HEY[k < 1, 230 >] BEAR[s < 1, 230 >]MAYBE[s < 1, 80 >] DROP[s < 1, 110 >]WITHIN[s < 1, 140 >] FIVE[s < 1, 170 >]MILES[s < 1, 2000 >] OF[s < 1, 230 >]uh[s < 1, 270 >]
            meme = "[:nh]";
            string[] split = boring.Split(' ');
            Random r = new Random();
            foreach (var s in split)
            {
                meme += s + "[n<" + r.Next(800, 800) + "," + r.Next(200, 200) + "]";
            }
            return meme;
        }
        private void StebinLog(object o)
        {
            StebinPipe1.WriteLine(o.ToString());
        }
        //private Process attachToThisProcess = null;
        //private IntPtr hwnd = new IntPtr();
        private WindowScrape.Types.HwndObject hwndobject = null;
        bool firstboy = true;


       
        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

           // m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
            m_GlobalHook.KeyUp += GlobalHookKeyUp;
           
    }
        private void GlobalHookKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var x = e.KeyCode;
            if (x == Keys.RShiftKey)
            {
                PopDownAttachBoy();
            }
        }

        private void PopUpAttachBoy()
        {
            attachboy.FixLocation();
            attachboy.StebinTextBox.Text = "";
            attachboy.Show();
        }
        private void PopDownAttachBoy()
        {
            attachboy.Hide();
            //now speak the text!)
            string text = (string)(attachboy.StebinTextBox.Text);

//            hwndobject.

            if (text == "")
                return;
            if (MoonbaseCheckbox.IsChecked == true)
                text = memetext(text);

            //save as a sound file
            const string abusedFile = "abusedFile_overlay.wav";
            using (var tts = new FonixTalkEngine())
            {

                tts.Voice = (TtsVoice)Enum.Parse(typeof(TtsVoice), VoiceSelector.Text);


                tts.SpeakToWavFile(abusedFile, text);
            }

            if (OverlayPlaybackCheckbox.IsChecked == true)
            {
                //System.IO.File.Copy(abusedFile, "copyboy", true);
                //playback on speaker
                string SelectedName = abusedFile;
                Thread thread = new Thread(new ParameterizedThreadStart((__SelectedName) =>
                {
                    string _SelectedName = (string)__SelectedName;
                    //https://github.com/naudio/NAudio
                    try
                    {
                        using (var audioFile = new AudioFileReader(_SelectedName))
                        {
                            int selDevice = -1;
                            
                            {
                                for (int n = -1; n < WaveOut.DeviceCount; n++)
                            {
                                var caps = WaveOut.GetCapabilities(n);
                                    if (caps.ProductName.StartsWith(CURRENT_SPEAKER_VALUE))
                                {
                                    selDevice = n;
                                    break;
                                }
                            }
                            }
                            using (var outputDevice = new WaveOutEvent()
                            {
                                DeviceNumber = selDevice
                            })
                            {

                                outputDevice.Init(audioFile);
                                outputDevice.Volume = (float)percentagevolume;
                                outputDevice.Play();
                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                    }
                    catch (System.Runtime.InteropServices.COMException e3)
                    { Console.WriteLine(e3); }
                }));
                thread.Start(SelectedName);
            }

           
            //////////
            //now output from the sound file
            using (var audioFile = new AudioFileReader(abusedFile))
            {
                int selDevice = -1;
                
                {
                    for (int n = -1; n < WaveOut.DeviceCount; n++)
                {
                    var caps = WaveOut.GetCapabilities(n);
                    if (caps.ProductName.StartsWith(CURRENT_MIC_VALUE))
                    {
                        selDevice = n;
                        break;
                    }
                }
                }
                using (var outputDevice = new WaveOutEvent()
                {
                    DeviceNumber = selDevice
                })
                {
                    PressAppropriatePTTButton(true);
                    outputDevice.Init(audioFile);
                    outputDevice.Volume = (float)percentagevolume;
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(1000);
                    }
                    PressAppropriatePTTButton(false);
                }
            }
            


        }
        private PTTSettingsWindow pttSettingsWindow = null;

        private void GlobalHookKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            var x = e.KeyCode;

            switch (sm_kp)
            {
                case _sm_kp.GET_BINDING:
                    pttSettingsWindow.KeyBindingTextBox.Text = ""+e.KeyCode;
                    sm_kp = _sm_kp.NORMAL;
                    e.Handled = true;
                    break;

                case _sm_kp.NORMAL:
                    if (x == Keys.RShiftKey)
                    {
                        PopUpAttachBoy();
                    }

                    if (x == Keys.Z)
                    {
                        //debug
                        //sendkeydown doesnt work
                        //cSendInput.SendKeyDown(cSendInput.VKeys.VK_P);
                        //Console.WriteLine((hwndobject.Hwnd));
                        //cSendInput.PostMessage_PressKeyDown((int)hwndobject.Hwnd, cSendInput.VKeys.VK_P, 0x190001);


                        e.Handled = true;
                    }

                    if (attachboy.IsVisible == true)
                    {
                        if ((int)x >= 65 && (int)x <= 90)
                        {
                            attachboy.StebinTextBox.Text += e.KeyCode;
                        }
                        if ((int)x == VKeys.VK_SPACE)
                        {
                            attachboy.StebinTextBox.Text += " ";
                        }
                    }

                    break;
            }
            
           
        }


        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

        //namespace PInvokeSample
        //{
        //    internal static class NativeMethods
        //    {

        //        [DllImport("User32.dll")]
        //        [return: MarshalAs(UnmanagedType.Bool)]
        //        internal static extern bool GetKeyboardState(byte[] lpKeyState);

        //    }
        //}
        private AttachWindow attachboy;
        private void begin()
        {
            //TextBoxStreamWriter t = new TextBoxStreamWriter(this.Dispatcher, OutputBoxHandle);
            pttSettingsWindow = new PTTSettingsWindow(this);


            attachboy = new AttachWindow();
            Subscribe();

            //firstboy = false;
            //https://github.com/DataDink/WindowScrape/blob/master/Source/WindowScrape/Types/HwndObject.cs
            //hwndobject = WindowScrape.Types.HwndObject.GetWindowByTitle("Realm of the Mad God");
            hwndobject = WindowScrape.Types.HwndObject.GetWindowByTitle("Counter-Strike: Global Offensive");
            //hwndobject = WindowScrape.Types.HwndObject.GetWindowByTitle("Untitled - Notepad");
            attachboy.AttachTo(hwndobject);


            STILL_LOADING_BTW = false;





        }
        private void Log(object o)
        {
            Console.WriteLine(o.ToString());
        }
        
        double percentagevolume;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;

                switch (button.Name)
                {
                    case "Ab0rt":

                        break;
                    case "IsraelNuke":

                        var text = Richy.Text;
                        if (MoonbaseCheckbox.IsChecked == true)
                            text = memetext(text);
                        //save as a sound file
                        const string abusedFile = "abusedFile.wav";
                        using (var tts = new FonixTalkEngine())
                        {

                            tts.Voice = (TtsVoice)Enum.Parse(typeof(TtsVoice), VoiceSelector.Text);


                            tts.SpeakToWavFile(abusedFile, text);
                        }

                        if (LucciHear.IsChecked == true)
                        {
                            Console.WriteLine("how the fuck ise this happen?");
                            //System.IO.File.Copy(abusedFile, "copyboy", true);
                            //playback on speaker
                            string SelectedName = abusedFile;
                        Thread thread = new Thread(new ParameterizedThreadStart((__SelectedName) =>
                        {
                            string _SelectedName = (string)__SelectedName;
                            //https://github.com/naudio/NAudio
                            try
                            {
                                using (var audioFile = new AudioFileReader(_SelectedName))
                                {
                                    int selDevice = -1;
                                        
                                       {
                                    for (int n = -1; n < WaveOut.DeviceCount; n++)
                                    {
                                        var caps = WaveOut.GetCapabilities(n);
                                           if (caps.ProductName.StartsWith(CURRENT_SPEAKER_VALUE))                                           {
                                                   
                                                   selDevice = n;
                                               break;
                                           }
                                    }
                                       }
                                    Console.WriteLine("ree+ " + selDevice);
                                    using (var outputDevice = new WaveOutEvent()
                                    {
                                        DeviceNumber = selDevice
                                    })
                                    {

                                        outputDevice.Init(audioFile);
                                        outputDevice.Volume = (float)percentagevolume;
                                        outputDevice.Play();
                                        while (outputDevice.PlaybackState == PlaybackState.Playing)
                                        {
                                            Thread.Sleep(1000);
                                        }
                                    }
                                }
                            }
                            catch (System.Runtime.InteropServices.COMException e3)
                            { Console.WriteLine(e3); }
                        }));
                        thread.Start(SelectedName);
                        }
                        //////////
                        //now output from the sound file
                        using (var audioFile = new AudioFileReader(abusedFile))
                        {
                            int selDevice = -1;
                            
                            
                            {
                                Console.WriteLine("THIS SHOULD BE FIRST");
                                for (int n = -1; n < WaveOut.DeviceCount; n++)
                            {
                                var caps = WaveOut.GetCapabilities(n);
                                    Console.WriteLine(caps.ProductName + ", " + CURRENT_MIC_VALUE);
                                if (caps.ProductName.StartsWith(CURRENT_MIC_VALUE))
                                {
                                        
                                    selDevice = n;
                                    break;
                                }
                            }
                            }
                            using (var outputDevice = new WaveOutEvent()
                            {
                                DeviceNumber = selDevice
                            })
                            {
                                PressAppropriatePTTButton(true);
                                outputDevice.Init(audioFile);
                                outputDevice.Volume = (float)percentagevolume;
                                outputDevice.Play();
                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    Thread.Sleep(1000);
                                }
                                PressAppropriatePTTButton(false);
                            }
                        }
                        break;



                }
            }catch(Exception exception)
            {
                Log("ERROR:" + exception.Message + ": " + exception.StackTrace);
            }
        }
        public enum _sm_kp 
        {
            GET_BINDING,
            NORMAL,
        } ;
        public _sm_kp sm_kp = _sm_kp.NORMAL;
        private void PressAppropriatePTTButton(bool down)
        {
            //cSendInput.CurrentWindowText()
            if (down == true)
            {
                //cSendInput.SendKeyDown(cSendInput.VKeys.VK_P);
                KeyboardSimulator.KeyDown(Keys.P);
            }
            else
            {
                // cSendInput.SendKeyUp(cSendInput.VKeys.VK_P);
                KeyboardSimulator.KeyUp(Keys.P);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            percentagevolume = shroud.Value / 10.0;
            SaveSettingsToFile();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        public static class VKeys
        {
            public const int
            VK_LBUTTON = 0x01,   //Left mouse button 
            VK_RBUTTON = 0x02,   //Right mouse button 
            VK_CANCEL = 0x03,   //Control-break processing 
            VK_MBUTTON = 0x04,   //Middle mouse button (three-button mouse) 
            VK_BACK = 0x08,   //BACKSPACE key 
            VK_TAB = 0x09,   //TAB key 
            VK_CLEAR = 0x0C,   //CLEAR key 
            VK_RETURN = 0x0D,   //ENTER key 
            VK_SHIFT = 0x10,   //SHIFT key 
            VK_CONTROL = 0x11,   //CTRL key 
            VK_MENU = 0x12,   //ALT key 
            VK_PAUSE = 0x13,   //PAUSE key 
            VK_CAPITAL = 0x14,   //CAPS LOCK key 
            VK_ESCAPE = 0x1B,   //ESC key 
            VK_SPACE = 0x20,   //SPACEBAR 
            VK_PRIOR = 0x21,   //PAGE UP key 
            VK_NEXT = 0x22,   //PAGE DOWN key 
            VK_END = 0x23,   //END key 
            VK_HOME = 0x24,   //HOME key 
            VK_LEFT = 0x25,   //LEFT ARROW key 
            VK_UP = 0x26,   //UP ARROW key 
            VK_RIGHT = 0x27,   //RIGHT ARROW key 
            VK_DOWN = 0x28,   //DOWN ARROW key 
            VK_SELECT = 0x29,   //SELECT key 
            VK_PRINT = 0x2A,   //PRINT key
            VK_EXECUTE = 0x2B,   //EXECUTE key 
            VK_SNAPSHOT = 0x2C,   //PRINT SCREEN key 
            VK_INSERT = 0x2D,   //INS key 
            VK_DELETE = 0x2E,   //DEL key 
            VK_HELP = 0x2F,   //HELP key
            VK_0 = 0x30,   //0 key 
            VK_1 = 0x31,   //1 key 
            VK_2 = 0x32,   //2 key 
            VK_3 = 0x33,   //3 key 
            VK_4 = 0x34,   //4 key 
            VK_5 = 0x35,   //5 key 
            VK_6 = 0x36,    //6 key 
            VK_7 = 0x37,    //7 key 
            VK_8 = 0x38,   //8 key 
            VK_9 = 0x39,    //9 key 
            VK_A = 0x41,   //A key 
            VK_B = 0x42,   //B key 
            VK_C = 0x43,   //C key 
            VK_D = 0x44,   //D key 
            VK_E = 0x45,   //E key 
            VK_F = 0x46,   //F key 
            VK_G = 0x47,   //G key 
            VK_H = 0x48,   //H key 
            VK_I = 0x49,    //I key 
            VK_J = 0x4A,   //J key 
            VK_K = 0x4B,   //K key 
            VK_L = 0x4C,   //L key 
            VK_M = 0x4D,   //M key 
            VK_N = 0x4E,    //N key 
            VK_O = 0x4F,   //O key 
            VK_P = 0x50,    //P key 
            VK_Q = 0x51,   //Q key 
            VK_R = 0x52,   //R key 
            VK_S = 0x53,   //S key 
            VK_T = 0x54,   //T key 
            VK_U = 0x55,   //U key 
            VK_V = 0x56,   //V key 
            VK_W = 0x57,   //W key 
            VK_X = 0x58,   //X key 
            VK_Y = 0x59,   //Y key 
            VK_Z = 0x5A,    //Z key
            VK_NUMPAD0 = 0x60,   //Numeric keypad 0 key 
            VK_NUMPAD1 = 0x61,   //Numeric keypad 1 key 
            VK_NUMPAD2 = 0x62,   //Numeric keypad 2 key 
            VK_NUMPAD3 = 0x63,   //Numeric keypad 3 key 
            VK_NUMPAD4 = 0x64,   //Numeric keypad 4 key 
            VK_NUMPAD5 = 0x65,   //Numeric keypad 5 key 
            VK_NUMPAD6 = 0x66,   //Numeric keypad 6 key 
            VK_NUMPAD7 = 0x67,   //Numeric keypad 7 key 
            VK_NUMPAD8 = 0x68,   //Numeric keypad 8 key 
            VK_NUMPAD9 = 0x69,   //Numeric keypad 9 key 
            VK_SEPARATOR = 0x6C,   //Separator key 
            VK_SUBTRACT = 0x6D,   //Subtract key 
            VK_DECIMAL = 0x6E,   //Decimal key 
            VK_DIVIDE = 0x6F,   //Divide key
            VK_F1 = 0x70,   //F1 key 
            VK_F2 = 0x71,   //F2 key 
            VK_F3 = 0x72,   //F3 key 
            VK_F4 = 0x73,   //F4 key 
            VK_F5 = 0x74,   //F5 key 
            VK_F6 = 0x75,   //F6 key 
            VK_F7 = 0x76,   //F7 key 
            VK_F8 = 0x77,   //F8 key 
            VK_F9 = 0x78,   //F9 key 
            VK_F10 = 0x79,   //F10 key 
            VK_F11 = 0x7A,   //F11 key 
            VK_F12 = 0x7B,   //F12 key
            VK_SCROLL = 0x91,   //SCROLL LOCK key 
            VK_LSHIFT = 0xA0,   //Left SHIFT key
            VK_RSHIFT = 0xA1,   //Right SHIFT key
            VK_LCONTROL = 0xA2,   //Left CONTROL key
            VK_RCONTROL = 0xA3,    //Right CONTROL key
            VK_LMENU = 0xA4,      //Left MENU key
            VK_RMENU = 0xA5,   //Right MENU key
            VK_PLAY = 0xFA,   //Play key
            VK_ZOOM = 0xFB; //Zoom key 
        }
        private string CURRENT_MIC_VALUE, CURRENT_SPEAKER_VALUE;
        private void MicSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CURRENT_MIC_VALUE = (string)(MicSelector.SelectedValue);
            SaveSettingsToFile();
        }

        private void VoiceSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SaveSettingsToFile();
        }

        private void OverlayPlaybackCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            SaveSettingsToFile();
        }

        private void LucciHear_Checked(object sender, RoutedEventArgs e)
        {
            SaveSettingsToFile();
        }

        private void MoonbaseCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            SaveSettingsToFile();
        }

        private void PttSettingsPressed(object sender, RoutedEventArgs e)
        {
            pttSettingsWindow.Left = this.Left + 30;
            pttSettingsWindow.Top = this.Top + 30;
            pttSettingsWindow.Show();
        }

        private void SpeakerSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CURRENT_SPEAKER_VALUE = (string)(SpeakerSelector.SelectedValue);
            SaveSettingsToFile();
        }
    }
}
