﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Core;

namespace TudoMario
{
    [Flags]
    public enum KeyAction { Up = 1, Down = 2, Left = 4, Right = 8 }

    public static class UserControlHandler
    {
        public static KeyAction PressedKeys { get; private set; } = 0;

        static UserControlHandler()
        {
            Window.Current.CoreWindow.KeyDown += UserKeyDown;
            Window.Current.CoreWindow.KeyUp += UserKeyUp;
        }

        private static void UserKeyDown(CoreWindow sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.W: PressedKeys |= KeyAction.Up; break;
                case VirtualKey.A: PressedKeys |= KeyAction.Left; break;
                case VirtualKey.S: PressedKeys |= KeyAction.Down; break;
                case VirtualKey.D: PressedKeys |= KeyAction.Right; break;
                case VirtualKey.Space: PressedKeys |= KeyAction.Up; break;
                case VirtualKey.GamepadA: PressedKeys |= KeyAction.Up; break;
                case VirtualKey.GamepadLeftThumbstickLeft: PressedKeys |= KeyAction.Left; break;
                case VirtualKey.GamepadLeftThumbstickDown: PressedKeys |= KeyAction.Down; break;
                case VirtualKey.GamepadLeftThumbstickRight: PressedKeys |= KeyAction.Up; break;
            }
        }

        private static void UserKeyUp(CoreWindow sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.W: PressedKeys &= ~KeyAction.Up; break;
                case VirtualKey.A: PressedKeys &= ~KeyAction.Left; break;
                case VirtualKey.S: PressedKeys &= ~KeyAction.Down; break;
                case VirtualKey.D: PressedKeys &= ~KeyAction.Right; break;
                case VirtualKey.Space: PressedKeys &= ~KeyAction.Up; break;
                case VirtualKey.GamepadA: PressedKeys &= ~KeyAction.Up; break;
                case VirtualKey.GamepadLeftThumbstickLeft: PressedKeys &= ~KeyAction.Left; break;
                case VirtualKey.GamepadLeftThumbstickDown: PressedKeys &= ~KeyAction.Down; break;
                case VirtualKey.GamepadLeftThumbstickRight: PressedKeys &= ~KeyAction.Right; break;
            }
        }
    }
}