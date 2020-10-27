using System;
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
    public enum KeyAction { UP, DOWN, LEFT, RIGHT }

    public class UserControlHandler
    {
        public List<KeyAction> PressedKeys { get; set; } = new List<KeyAction>();
        public UserControlHandler()
        {
            Window.Current.CoreWindow.KeyDown += UserKeyDown;
            Window.Current.CoreWindow.KeyUp += UserKeyUp;
        }

        private void UserKeyDown(CoreWindow sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.W: PressedKeys.Add(KeyAction.UP); break;
                case VirtualKey.A: PressedKeys.Add(KeyAction.LEFT); break;
                case VirtualKey.S: PressedKeys.Add(KeyAction.DOWN); break;
                case VirtualKey.D: PressedKeys.Add(KeyAction.RIGHT); break;
                case VirtualKey.Space: PressedKeys.Add(KeyAction.UP); break;
                case VirtualKey.GamepadA: PressedKeys.Add(KeyAction.UP); break;
                case VirtualKey.GamepadLeftThumbstickLeft: PressedKeys.Add(KeyAction.LEFT); break;
                case VirtualKey.GamepadLeftThumbstickDown: PressedKeys.Add(KeyAction.DOWN); break;
                case VirtualKey.GamepadLeftThumbstickRight: PressedKeys.Add(KeyAction.RIGHT); break;
            }
        }
        private void UserKeyUp(CoreWindow sender, KeyEventArgs e)
        {
            switch (e.VirtualKey)
            {
                case VirtualKey.W: PressedKeys.Remove(KeyAction.UP); break;
                case VirtualKey.A: PressedKeys.Remove(KeyAction.LEFT); break;
                case VirtualKey.S: PressedKeys.Remove(KeyAction.DOWN); break;
                case VirtualKey.D: PressedKeys.Remove(KeyAction.RIGHT); break;
                case VirtualKey.Space: PressedKeys.Remove(KeyAction.UP); break;
                case VirtualKey.GamepadA: PressedKeys.Remove(KeyAction.UP); break;
                case VirtualKey.GamepadLeftThumbstickLeft: PressedKeys.Remove(KeyAction.LEFT); break;
                case VirtualKey.GamepadLeftThumbstickDown: PressedKeys.Remove(KeyAction.DOWN); break;
                case VirtualKey.GamepadLeftThumbstickRight: PressedKeys.Remove(KeyAction.RIGHT); break;
            }
        }
    }
}