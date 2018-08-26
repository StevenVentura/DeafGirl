using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IceChips
{
    public class BindingBoy
    {
        public string WindowName { get; set; }
        public string Binding { get; set; }
        public string BindingName { get; set; }
        public static string GetBindingName(string Binding)
        {
            //convert int to VKeys enum name
            return ""+(Keys)Enum.Parse(typeof(Keys), Binding);
        }
        public static byte ReverseAgain(string BindingName)
        {
            //convert name to int
            return (byte)((Keys)Enum.Parse(typeof(Keys), BindingName.ToUpper()));
        }
    }
}
