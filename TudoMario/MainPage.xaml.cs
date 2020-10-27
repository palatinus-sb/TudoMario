using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TudoMario.Map;
using TudoMario.Ui;
using TudoMario.Rendering;
using Windows.UI.Core;
using Windows.Services.Store;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TudoMario
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        UiController uicontroller;
        Renderer renderer;
        public Grid MainGrid
        {
            get
            {
                return UiMainGrid;
            }
        }
        public MainPage()
        {
            InitializeComponent();

            bool ControlLeftPushed = false;
            bool ControlRightPushed = false;
            bool ControlUpPushed = false;
            bool ControlDownPushed = false;

            LogicController logiccontroller = new LogicController();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
            {
                string KeyPressed = e.VirtualKey.ToString();
                if ("W" == KeyPressed || "Space" == KeyPressed)
                {
                    ControlUpPushed = true;
                }
                if ("A" == KeyPressed)
                {
                    ControlLeftPushed = true;
                }
                if ("S" == KeyPressed)
                {
                    ControlDownPushed = true;
                }
                if ("D" == KeyPressed)
                {
                    ControlRightPushed = true;
                }
                if (ControlLeftPushed && ControlRightPushed)
                {
                    ControlLeftPushed = false;
                    ControlRightPushed = false;
                }
            }

            void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs e)
            {
                string KeyPressed = e.VirtualKey.ToString();
                if ("W" == KeyPressed || "Space" == KeyPressed)
                {
                    ControlUpPushed = false;
                }
                if ("A" == KeyPressed)
                {
                    ControlLeftPushed = false;
                }
                if ("S" == KeyPressed)
                {
                    ControlDownPushed = false;
                }
                if ("D" == KeyPressed)
                {
                    ControlRightPushed = false;
                }
            }

            switch (Configuration.Dev)
            {
                case Configuration.Developer.Adam:
                    renderer = new Renderer(this);
                    uicontroller = new UiController(this,renderer);
                    break;
                case Configuration.Developer.Dani:
                    break;
                case Configuration.Developer.Patrik:
                    break;
                case Configuration.Developer.Soma:
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cont = ((Button)sender).Content.ToString();
            uicontroller.Testf(cont);
        }
    }
}
