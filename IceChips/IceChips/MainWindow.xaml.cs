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
            this.begin();
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
        private void begin()
        {
            TextBoxStreamWriter t = new TextBoxStreamWriter(this.Dispatcher, OutputBoxHandle);
            TextBoxStreamWriter stebinpipelol = new TextBoxStreamWriter(this.Dispatcher, Richy, true);
            foreach (var value in Enum.GetValues(typeof(TtsVoice)))
                VoiceSelector.Items.Add(value);
            VoiceSelector.SelectedIndex = 0;
            new Thread(new ThreadStart(() =>
           {
               while (true)
               {
                   Thread.Sleep(250);
                   if (firstboy)
                   {
                       firstboy = false;
                       var hwndobject = WindowScrape.Types.HwndObject.GetWindowByTitle("Realm of the Mad God");
                       hwndobject.Location = new System.Drawing.Point(0, 0);

                   }


               }
           })).Start();
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
                Button button = (Button)sender;

                switch (button.Name)
                {
                    case "Ab0rt":

                        break;
                    case "IsraelNuke":

                        var text = Richy.Text;
                        //save as a sound file
                        const string abusedFile = "abusedFile.wav";
                        using (var tts = new FonixTalkEngine())
                        {

                            tts.Voice = (TtsVoice)Enum.Parse(typeof(TtsVoice), VoiceSelector.Text);


                            tts.SpeakToWavFile(abusedFile, text);
                        }

                        if (LucciHear.IsChecked == true)
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
                                    for (int n = -1; n < WaveOut.DeviceCount; n++)
                                    {
                                        var caps = WaveOut.GetCapabilities(n);
                                        if (caps.ProductName.Contains("Headset Earphone"))
                                        {
                                            selDevice = n;
                                            break;
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
                            for (int n = -1; n < WaveOut.DeviceCount; n++)
                            {
                                var caps = WaveOut.GetCapabilities(n);
                                if (caps.ProductName.Contains("CABLE Input"))
                                {
                                    selDevice = n;
                                    break;
                                }
                            }
                            using (var outputDevice = new WaveOutEvent()
                            {
                                DeviceNumber = selDevice
                            })
                            {
                                PressAppropriatePTTButton();
                                outputDevice.Init(audioFile);
                                outputDevice.Volume = (float)percentagevolume;
                                outputDevice.Play();
                                while (outputDevice.PlaybackState == PlaybackState.Playing)
                                {
                                    Thread.Sleep(1000);
                                }
                            }
                        }
                        break;



                }
            }catch(Exception exception)
            {
                Log("ERROR:" + exception.Message + ": " + exception.StackTrace);
            }
        }

        private void PressAppropriatePTTButton()
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            percentagevolume = shroud.Value / 10.0;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
