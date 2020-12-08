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
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TudoMario
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Renderer renderer;
        private MapBase map;
        private UiController uicontroller;

        public Grid MainGrid { get => UiMainGrid; }

        public MainPage()
        {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            InitializeComponent();

            renderer = new Renderer(this);
            map = new MapBase(new Vector2(0, 0));
            uicontroller = new UiController(this, renderer);

            LogicController logiccontroller = new LogicController(renderer, map, uicontroller);
            logiccontroller.StartGame();

            switch (Configuration.Dev)
            {
                case Configuration.Developer.Adam:
                    break;
                case Configuration.Developer.Dani:
                    break;
                case Configuration.Developer.Patrik:
                    break;
                case Configuration.Developer.Soma:
                    break;
            }

            Window.Current.CoreWindow.KeyDown += Page_KeyDown;
            Window.Current.CoreWindow.KeyUp += Page_KeyUp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cont = ((Button)sender).Content.ToString();
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            UserControlHandler.UserKeyDown(e.VirtualKey);
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            UserControlHandler.UserKeyUp(e.VirtualKey);
        }
    }
}
