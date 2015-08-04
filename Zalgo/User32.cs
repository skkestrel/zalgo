using System;
using System.Runtime.InteropServices;

namespace Zalgo
{
	static class User32
	{
		[StructLayout(LayoutKind.Sequential)]
		public class KBDLLHOOKSTRUCT
		{
			public uint vkCode;
			public uint scanCode;
			public KBDLLHOOKSTRUCTFlags flags;
			public uint time;
			public UIntPtr dwExtraInfo;
		}

		[Flags]
		public enum KBDLLHOOKSTRUCTFlags : uint
		{
			LLKHF_EXTENDED = 0x01,
			LLKHF_INJECTED = 0x10,
			LLKHF_ALTDOWN = 0x20,
			LLKHF_UP = 0x80,
		}

		public enum WM : uint
		{
			NULL = 0x0000,
			KEYDOWN = 0x0100,
			USER = 0x0400,
		}

		[DllImport("user32.dll")]
		public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		public enum HookType : int
		{
			WH_JOURNALRECORD = 0,
			WH_JOURNALPLAYBACK = 1,
			WH_KEYBOARD = 2,
			WH_GETMESSAGE = 3,
			WH_CALLWNDPROC = 4,
			WH_CBT = 5,
			WH_SYSMSGFILTER = 6,
			WH_MOUSE = 7,
			WH_HARDWARE = 8,
			WH_DEBUG = 9,
			WH_SHELL = 10,
			WH_FOREGROUNDIDLE = 11,
			WH_CALLWNDPROCRET = 12,
			WH_KEYBOARD_LL = 13,
			WH_MOUSE_LL = 14
		}

		public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput
		(
			uint nInputs,
			[MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs,
			int cbSize
		);

		[StructLayout(LayoutKind.Sequential)]
		public struct INPUT
		{
			public uint type;
			public InputUnion U;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct InputUnion
		{
			[FieldOffset(0)]
			public MOUSEINPUT mi;
			[FieldOffset(0)]
			public KEYBDINPUT ki;
			[FieldOffset(0)]
			public HARDWAREINPUT hi;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MOUSEINPUT
		{
			int dx;
			int dy;
			int mouseData;
			uint dwFlags;
			uint time;
			UIntPtr dwExtraInfo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct KEYBDINPUT
		{
			public VirtualKeyShort wVk;
			public ScanCodeShort wScan;
			public KEYEVENTF dwFlags;
			public int time;
			public UIntPtr dwExtraInfo;
		}

		[Flags]
		public enum KEYEVENTF : uint
		{
			EXTENDEDKEY = 0x0001,
			KEYUP = 0x0002,
			SCANCODE = 0x0008,
			UNICODE = 0x0004
		}

		public enum VirtualKeyShort : short
		{
		}
		public enum ScanCodeShort : short
		{
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct HARDWAREINPUT
		{
			public int uMsg;
			public short wParamL;
			public short wParamH;
		}
	}
}
