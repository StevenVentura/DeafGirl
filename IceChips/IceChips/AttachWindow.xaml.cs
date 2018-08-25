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
using System.Windows.Shapes;

namespace IceChips
{
    /// <summary>
    /// Interaction logic for AttachWindow.xaml
    /// </summary>
    public partial class AttachWindow : Window
    {
        public AttachWindow()
        {
            InitializeComponent();
            
            this.Loaded += OnLoad;
        }
        public void OnLoad(object o1, object o2)
        {
            TextBoxStreamWriter stebinpipelol = new TextBoxStreamWriter(this.Dispatcher, StebinTextBox, true);
        }
        private void StebinLog(object o)
        {
            StebinPipe1.WriteLine(o.ToString());
        }
        public void FixLocation()
        {
            this.Top = this.hwndobject.Location.Y;
            this.Left = this.hwndobject.Location.X;
            this.Topmost = true;
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }

        //https://github.com/DataDink/WindowScrape/blob/master/Source/WindowScrape/Types/HwndObject.cs
        public WindowScrape.Types.HwndObject hwndobject;
        public void AttachTo(WindowScrape.Types.HwndObject hwndobject)
        {
            this.hwndobject = hwndobject;
            this.Top = hwndobject.Location.Y;
            this.Left = hwndobject.Location.X;
        }
        


    }
}
