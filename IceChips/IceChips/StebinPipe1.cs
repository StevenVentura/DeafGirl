using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace IceChips
{
    public static class StebinPipe1
    {
    
        private static TextWriter textWriter = null;
        
        public static void SetOut(TextWriter _TextWriter)
        {
            textWriter = _TextWriter;
        }
        public static void WriteLine(Object o)
        {
            textWriter.WriteLine(o.ToString());
        }
    }
}
