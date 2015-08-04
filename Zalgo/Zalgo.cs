using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Diagnostics;

namespace Zalgo
{
	public partial class ZalgoService : Form
	{
		private readonly uint[] IgnoredScanCodes =
		{
			0x01, 0x0E, 0x0F, // ESC, BKSP, TAB
			0x1C, 0x1D, 0x2A, // ENTER, L/RCTRL, LSHIFT
			0x35, 0x36, 0x37, 0x38, // GRAY, RSHIFT, PRISC, L/RALT
			0x39, 0x3A, // SPACE, CAPS
			0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, // etc
			0x5B // WIN
		};

		private readonly char[] ZalgoUp =
		{
			'\u030d',   '\u030e',   '\u0304',   '\u0305',
			'\u033f',   '\u0311',   '\u0306',   '\u0310',
			'\u0352',   '\u0357',   '\u0351',   '\u0307',
			'\u0308',   '\u030a',   '\u0342',   '\u0343',
			'\u0344',   '\u034a',   '\u034b',   '\u034c',
			'\u0303',   '\u0302',   '\u030c',   '\u0350',
			'\u0300',   '\u0301',   '\u030b',   '\u030f',
			'\u0312',   '\u0313',   '\u0314',   '\u033d',
			'\u0309',   '\u0363',   '\u0364',   '\u0365',
			'\u0366',   '\u0367',   '\u0368',   '\u0369',
			'\u036a',   '\u036b',   '\u036c',   '\u036d',
			'\u036e',   '\u036f',   '\u033e',   '\u035b',
		};
		private readonly char[] ZalgoDown =
		{
			'\u0316',   '\u0317',   '\u0318',   '\u0319',
			'\u031c',   '\u031d',   '\u031e',   '\u031f',
			'\u0320',   '\u0324',   '\u0325',   '\u0326',
			'\u0329',   '\u032a',   '\u032b',   '\u032c',
			'\u032d',   '\u032e',   '\u032f',   '\u0330',
			'\u0331',   '\u0332',   '\u0333',   '\u0339',
			'\u033a',   '\u033b',   '\u033c',   '\u0345',
			'\u0347',   '\u0348',   '\u0349',   '\u034d',
			'\u034e',   '\u0353',   '\u0354',   '\u0355',
			'\u0356',   '\u0359',   '\u035a',   '\u0323'
		};
		private readonly char[] ZalgoMid =
		{
			'\u0315',   '\u031b',   '\u0340',   '\u0341',
			'\u0358',   '\u0321',   '\u0322',   '\u0327',
			'\u0328',   '\u0334',   '\u0335',   '\u0336',
			'\u034f',   '\u035c',   '\u035d',   '\u035e',
			'\u035f',   '\u0360',   '\u0362',   '\u0338',
			'\u0337',   '\u0361',   '\u0489'
		};

		private User32.HookProc _llKeyboardProc;
		private IntPtr _hookHandle;
		private Random _random = new Random();
		private KeyboardHook _keyboardHook = new KeyboardHook();
		private bool _enable = false;

		public ZalgoService()
		{
			InitializeComponent();

			_llKeyboardProc = LLKeyboardProc;
		}

		private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
		{
			_enable = !_enable;
			_trayIcon.Icon = _enable ? Properties.Resources.EnabledIcon : Properties.Resources.DisabledIcon;
			_enabledLabel.Text = _enable ? "Enabled" : "Disabled";
		}

		private IntPtr LLKeyboardProc(int code, IntPtr wparam, IntPtr lparam)
		{
			if (code >= 0 && _enable)
			{
				if ((User32.WM) wparam == User32.WM.KEYDOWN)
				{
					User32.KBDLLHOOKSTRUCT kb = Marshal.PtrToStructure<User32.KBDLLHOOKSTRUCT>(lparam);
					if ((kb.flags & User32.KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED) == 0 &&
						!IgnoredScanCodes.Contains(kb.scanCode) &&
						(ModifierKeys & Keys.Control) == 0 &&
						(ModifierKeys & Keys.Alt) == 0)
					{
#if DEBUG
						Console.WriteLine(kb.scanCode);
#endif
						Thread t = new Thread(new ParameterizedThreadStart(SendZalgo));
						t.Start((int) _densityNumeric.Value);
					}
				}
			}
			return User32.CallNextHookEx(IntPtr.Zero, code, wparam, lparam);
		}

		private char GetZalgo()
		{
			char[] source;
			switch (_random.Next() % 3)
			{
				case 0:
					source = ZalgoUp;
					break;
				case 1:
					source = ZalgoDown;
					break;
				case 2:
				default:
					source = ZalgoMid;
					break;
			}

			return source[_random.Next() % source.Length];
		}

		private void SendZalgo(object amount)
		{
			Thread.Sleep(1); // wait for event to be registered

			User32.INPUT[] inputs = new User32.INPUT[(int) amount * 2];

			char previousZalgo = '\u0000';
			for (int i = 0; i < inputs.Length; i++)
			{
				if (i % 2 == 0) previousZalgo = GetZalgo();

				inputs[i].type = 1; // keyboard event
				inputs[i].U.ki.wVk = 0;
				inputs[i].U.ki.wScan = (User32.ScanCodeShort) previousZalgo;
				inputs[i].U.ki.dwFlags =
					i % 2 == 0 ?
					User32.KEYEVENTF.UNICODE :
					User32.KEYEVENTF.UNICODE | User32.KEYEVENTF.KEYUP;

				inputs[i].U.ki.time = 0;
				inputs[i].U.ki.dwExtraInfo = UIntPtr.Zero;
			}

			User32.SendInput((uint) inputs.Length, inputs, Marshal.SizeOf<User32.INPUT>());
		}

		private void ZalgoService_Load(object sender, EventArgs e)
		{
			_hookHandle = User32.SetWindowsHookEx(User32.HookType.WH_KEYBOARD_LL, _llKeyboardProc, IntPtr.Zero, 0); // (uint) Thread.CurrentThread.ManagedThreadId);
			_keyboardHook.KeyPressed += Hook_KeyPressed;
			_keyboardHook.RegisterHotKey(Zalgo.ModifierKeys.Control | Zalgo.ModifierKeys.Win, Keys.Z);
			_trayIcon.Icon = Properties.Resources.DisabledIcon;
		}

		private void ZalgoService_FormClosing(object sender, FormClosingEventArgs e)
		{
			User32.UnhookWindowsHookEx(_hookHandle);
		}

		private void ZalgoService_Resize(object sender, EventArgs e)
		{
			_trayIcon.BalloonTipTitle = "Zalgo";
			_trayIcon.BalloonTipText = "Zalgo will run in the background.";

			if (WindowState == FormWindowState.Minimized)
			{
				_trayIcon.ShowBalloonTip(1000);
				Hide();
			}
		}

		private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://pt-get.com");
		}

		private void TrayIcon_MouseClick(object sender, MouseEventArgs e)
		{
			Show();
			WindowState = FormWindowState.Normal;
		}
	}
}
