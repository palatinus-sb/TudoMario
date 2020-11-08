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
        public CoreApplicationView CurrentView { get; set; }

        public MainPage()
        {
            InitializeComponent();

            renderer = new Renderer(this);
            uicontroller = new UiController(this, renderer);
            uicontroller.CurrentView = CoreApplication.GetCurrentView();

            LogicController logiccontroller = new LogicController(renderer,uicontroller);
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cont = ((Button)sender).Content.ToString();
            uicontroller.Testf(cont);
        }
    }
}
