using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// 
/// </summary>
/// 
namespace BotPS2
{
    public class HardwareKeyboardInput
    {
        

    }
    public class cSendInput
    {
        #region DLLImports

        /*
         * // W key

        keybd_event(0x51, 0x11, 0, 0); //Key down
        keybd_event(0x51, 0x11, KEYEVENTF_KEYUP, 0); //Key up
        scan codes
        https://msdn.microsoft.com/en-us/library/aa299374(v=vs.60).aspx
         * */
        //https://ourcodeworld.com/articles/read/520/simulating-keypress-in-the-right-way-using-inputsimulator-with-csharp-in-winforms
        // Import the user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static byte GetHardwareScan(VKeys v)
        {
            switch (v)
            {
                //https://docs.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-6.0/aa299374(v=vs.60)
                case VKeys.VK_ESCAPE:return 0x1;

                case VKeys.VK_1:return 0x2;
                case VKeys.VK_2:return 0x3;
                case VKeys.VK_3:return 0x4;
                case VKeys.VK_4:return 0x5;
                case VKeys.VK_5:return 0x6;
                case VKeys.VK_6:return 0x7;
                case VKeys.VK_7:return 0x8;
                case VKeys.VK_8:return 0x9;
                case VKeys.VK_9:return 0xA;
                case VKeys.VK_SUBTRACT:return 0xC;
                case VKeys.VK_BACK: return 0xE;
                case VKeys.VK_TAB: return 0xF;
                case VKeys.VK_Q:return 0x10;
                case VKeys.VK_W:return 0x11;
                case VKeys.VK_E:return 0x12;
                case VKeys.VK_R:return 0x13;
                case VKeys.VK_T:return 0x14;
                case VKeys.VK_Y:return 0x15;
                case VKeys.VK_U: return 0x16;
                case VKeys.VK_I: return 0x17;
                case VKeys.VK_O: return 0x18;
                case VKeys.VK_P: return 0x19;
                case VKeys.VK_RETURN: return 0x1C;
                case VKeys.VK_CONTROL: return 0x1D;
                case VKeys.VK_A:return 0x1E;
                case VKeys.VK_S:return 0x1F;
                case VKeys.VK_D:return 0x20;
                case VKeys.VK_F:return 0x21;
                case VKeys.VK_G:return 0x22;
                case VKeys.VK_H:return 0x23;
                case VKeys.VK_J:return 0x24;
                case VKeys.VK_K:return 0x25;
                case VKeys.VK_L: return 0x26;
                //case VKeys.VK_COLON: return 0x27;
                //case VKeys.VK_QUOTE: return 0x28;
                //case VKeys.VK_TILDE: return 0x29;
                case VKeys.VK_LSHIFT: return 0x2A;
                //case VKeys.VK_BACKSLASH: return 0x2B;
                case VKeys.VK_Z: return 0x2C;
                case VKeys.VK_X: return 0x2D;
                case VKeys.VK_C: return 0x2E;
                case VKeys.VK_V: return 0x2F;
                case VKeys.VK_B: return 0x30;
                case VKeys.VK_N: return 0x31;
                case VKeys.VK_M: return 0x32;
                //case VKeys.VK_COMMA: return 0x33;
                case VKeys.VK_DECIMAL: return 0x34;
                //case VKeys.VK_QUESTION: return 0x35;
                case VKeys.VK_RSHIFT: return 0x36;
                //case VKeys.VK_LALT: return 0x38;
                //case VKeys.VK_RALT: return 0x38;
                case VKeys.VK_SPACE: return 0x39;
                case VKeys.VK_CAPITAL: return 0x3A;
                case VKeys.VK_F1: return 0x3B;
                case VKeys.VK_F2: return 0x3C;
                case VKeys.VK_F3: return 0x3D;
                case VKeys.VK_F4: return 0x3E;
                case VKeys.VK_F5: return 0x3F;
                case VKeys.VK_F6: return 0x40;
                case VKeys.VK_F7: return 0x41;
                case VKeys.VK_F8: return 0x42;
                case VKeys.VK_F9: return 0x43;
                case VKeys.VK_F10: return 0x44;
                case VKeys.VK_F11: return 0x57;
                case VKeys.VK_F12: return 0x58;
                //case VKeys.NUMLOCK return 0x45;
                case VKeys.VK_SCROLL: return 0x46;
                case VKeys.VK_HOME: return 0x47;
                case VKeys.VK_UP: return 0x48;
                case VKeys.VK_PRIOR: return 0x49;
                case VKeys.VK_LEFT: return 0x4B;
                //case VKeys.VK_CENTER: return 0x4C;
                case VKeys.VK_RIGHT: return 0x4D;
                case VKeys.VK_END: return 0x4F;
                case VKeys.VK_DOWN: return 0x50;
                case VKeys.VK_NEXT: return 0x51;
                case VKeys.VK_INSERT: return 0x52;
                case VKeys.VK_DELETE: return 0x53;
                default:return 0xFF;
            }
            
        }
        public const byte MagicCSGObVk = 0x51;
        public static void SendHardwareKeyDown(VKeys toscanboy)
        {
            Console.WriteLine("the hardware boy is " + GetHardwareScan(toscanboy));
            keybd_event(MagicCSGObVk, GetHardwareScan(toscanboy), 0, 0); //Key down
        }
        public static void SendHardwareKeyUp(VKeys toscanboy)
        {
            keybd_event(MagicCSGObVk, GetHardwareScan(toscanboy), 0x0002, 0); //Key up
        }


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("User32.Dll")]
        public static extern Int32 PostMessage(int hWnd, int msg, int wParam, int lParam);


        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //[DllImport("user32.dll")]
        //static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        #endregion

        /// <summary>
        /// Virtual Keys
        /// </summary>
        public enum VKeys : ushort
        {
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
            VK_ZOOM = 0xFB, //Zoom key 
        }

        ///summary>
        /// Virtual Messages
        /// </summary>
        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202,  //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,   //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100,  //Key down
            WM_KEYUP = 0x101,   //Key up
        }

        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        public static int CalculateAbsoluteCoordinateX(int x)
        {
            double precision = (double)x * 65536;
            precision = precision / (double)GetSystemMetrics(SystemMetric.SM_CXSCREEN);

            return (int)Math.Round(precision);

            //return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        public static int CalculateAbsoluteCoordinateY(int y)
        {
            double precision = (double)y * 65536;
            precision = precision / (double)GetSystemMetrics(SystemMetric.SM_CYSCREEN);

            return (int)Math.Round(precision);

            //return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public SendInputEventType type;
            public MouseKeybdhardwareInputUnion mkhi;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdhardwareInputUnion
        {
            [FieldOffset(0)]
            public MouseInputData mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        struct MouseInputData
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public MouseEventFlags dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [Flags]
        enum MouseEventFlags : uint
        {
            MOUSEEVENTF_MOVE = 0x0001,
            MOUSEEVENTF_LEFTDOWN = 0x0002,
            MOUSEEVENTF_LEFTUP = 0x0004,
            MOUSEEVENTF_RIGHTDOWN = 0x0008,
            MOUSEEVENTF_RIGHTUP = 0x0010,
            MOUSEEVENTF_MIDDLEDOWN = 0x0020,
            MOUSEEVENTF_MIDDLEUP = 0x0040,
            MOUSEEVENTF_XDOWN = 0x0080,
            MOUSEEVENTF_XUP = 0x0100,
            MOUSEEVENTF_WHEEL = 0x0800,
            MOUSEEVENTF_VIRTUALDESK = 0x4000,
            MOUSEEVENTF_ABSOLUTE = 0x8000
        }

        [Flags]
        enum KeyboardEventFlags : uint
        {
            WM_KEYUP = 0x0101,
            WM_KEYDOWN = 0x0100
        }

        enum SendInputEventType : int
        {
            InputMouse,
            InputKeyboard,
            InputHardware
        }

        public static void ClickLeftMouseButton(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }
        public static void ClickLeftMouseButtonDown(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        public static void ClickLeftMouseButtonUp(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        public static void ClickRightMouseButton(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;


            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_RIGHTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        public static void MoveMouse(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = CalculateAbsoluteCoordinateX(x);
            mouseInput.mkhi.mi.dy = CalculateAbsoluteCoordinateY(y);
            mouseInput.mkhi.mi.mouseData = 0;

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));
        }

        public static void SendKeyDown(VKeys virtualKey)
        {
            INPUT keyboardInput = new INPUT();
            keyboardInput.type = SendInputEventType.InputKeyboard;
            keyboardInput.mkhi.ki.wVk = (ushort)virtualKey;
            keyboardInput.mkhi.ki.wScan = 0;
            keyboardInput.mkhi.ki.dwFlags = (uint)(KeyboardEventFlags.WM_KEYDOWN);
            keyboardInput.mkhi.ki.time = 0;
            keyboardInput.mkhi.ki.dwExtraInfo = IntPtr.Zero;

            SendInput(1, ref keyboardInput, Marshal.SizeOf(new INPUT()));
        }

        public static void SendKeyUp(VKeys virtualKey)
        {
            INPUT keyboardInput = new INPUT();
            keyboardInput.type = SendInputEventType.InputKeyboard;
            keyboardInput.mkhi.ki.wVk = (ushort)virtualKey;
            keyboardInput.mkhi.ki.wScan = 0;
            keyboardInput.mkhi.ki.dwFlags = 0x0002;
            keyboardInput.mkhi.ki.time = 0;
            keyboardInput.mkhi.ki.dwExtraInfo = IntPtr.Zero;

            SendInput(1, ref keyboardInput, Marshal.SizeOf(new INPUT()));
        }

        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        public static string CurrentWindowText()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow != IntPtr.Zero)
            {
                string windowTitle = GetText(foregroundWindow).Trim();
                if (windowTitle == null)
                {
                    windowTitle = string.Empty;
                }
                return windowTitle;

            }
            return string.Empty;
        }

        public static IntPtr CurrentWindow()
        {
            return GetForegroundWindow();
        }

        public static void PostMessage_PressKeyDown(int hWnd, VKeys Key, int lParam)
        {
            PostMessage(hWnd, (int)WMessages.WM_KEYDOWN, (int)Key, lParam);
        }
        public static void PostMessage_PressKeyUp(int hWnd, VKeys Key, int lParam)
        {
            PostMessage(hWnd, (int)WMessages.WM_KEYUP, (int)Key, lParam);
        }

        public static void PostMessage_PressKey(int hWnd, VKeys Key, int lParam)
        {

            PostMessage(hWnd, (int)WMessages.WM_KEYDOWN, (int)Key, lParam);
            PostMessage(hWnd, (int)WMessages.WM_KEYUP, (int)Key, lParam);

            //---------------------------------------------------------------------------
            /*
            Key    wParam  lParam
            1      31      20001
            2      32      30001
            3      33      40001
            4      34      50001
            5      35      60001
            6      36      70001
            7      37      80001
            8      38      90001
            9      39      a0001
            -      bd      c0001
            ^      de      280001
            \      dc      2b0001
            q      51      100001
            w      57      110001
            e      45      120001
            r      52      130001
            t      54      140001
            y      59      150001
            u      55      160001
            i      49      170001
            o      4f      180001
            p      50      190001
            @      c0      290001
            [      db      1a0001
            a      41      1e0001
            s      53      1f0001
            d      44      200001
            f      46      210001
            g      47      220001
            h      48      230001
            j      4a      240001
            k      4b      250001
            l      4c      260001
            ;      bb      d0001
            :      ba      270001
            ]      dd      1b0001
            z      5a      2c0001
            x      58      2d0001
            c      43      2e0001
            v      56      2f0001
            b      42      300001
            n      4e      310001
            m      4d      320001
            ,      bc      330001
            .      be      340001
            /      bf      350001
            (_)    df      550001
            [ESC]  1b      10001
            [TAB]  9       f0001
            [CTRL-L] 11    1d0001
            [CAPS] 14      3a0001
            [SHIFT-L] 10   2a0001
            [??] 15      11d0001
            [WIN-L] 5b     15b0001
            [GRPH] ????
            [NFER] 1d      5a0001
            [SPACE] 20     390001
            [BS]   8       e0001
            [RET]  d       1c0001
            [SHIFT-R] 10   360001
            [XFER] 1c      1380001
            [WIN-R] 5c     15c0001
            [???]  5d      15d0001
            [INS]  2d      1520001
            [DEL]  2e      1530001
            [ROLLUP] 22    1510001
            [ROLLDN] 21    1490001
            [?]   26      1480001
            [?]   25      14b0001
            [?]   27      14d0001
            [?]   28      1500001
            [HOMECLR] 24   1470001
            [HELP] 23      14f0001
            [-]    6d      4a0001
            [/]    6f      1350001
            [*]    6a      370001
            [+]    6b      4e0001
            [=]    92      590001
            [RET]  d       1c0001
            [7]    67      470001
            [8]    68      480001
            [9]    69      490001
            [4]    64      4b0001
            [5]    65      4c0001
            [6]    66      4d0001
            [1]    61      4f0001
            [2]    62      500001
            [3]    63      510001
            [0]    60      520001
            [,]    6c      5c0001
            [.]    6e      530001
            [STOP] ????
            [COPY] ????
            [F1]   70      3b0001
            [F2]   71      3c0001
            [F3]   72      3d0001
            [F4]   73      3e0001
            [F5]   74      3f0001
            [F6]   75      400001
            [F7]   76      410001
            [F8]   77      420001
            [F9]   78      430001
            [F10] ????(ALT??????)
            [VF1]  7a      570001
            [VF2]  7b      580001
            [VF3]  7c      5d0001
            [VF4]  7d      5e0001
            [VF5]  7e      5f0001

            */


        }
    }
}