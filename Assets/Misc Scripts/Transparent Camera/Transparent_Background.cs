using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.XR;

public class Transparent_Background : MonoBehaviour
{
    //Example using various API's 
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    //This is used to make the window click thru.
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    //This is used to interact
    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);


    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;

    //This is used to run in the background.
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    //This is used to interact
    private const uint LWA_COLORKEY = 0x00000001;


    private struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

    private void Start()
    {
#if !UNITY_EDITOR

            IntPtr hWnd = GetActiveWindow();

            //Make the window background transparent.(Bare Minimum we need)
            Margins margins = new Margins { cxLeftWidth = -1 };
            DwmExtendFrameIntoClientArea(hWnd, ref margins);

            //Make the window Click thru but can be clicked on non transparent.
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
            SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);
            
            SetWindowPos(hWnd, HWND_TOPMOST, 0,0,0,0,0);
#endif
    }
}