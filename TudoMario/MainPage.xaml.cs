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
        Timer timer = new Timer(10);
        Stopwatch stopwatch = new Stopwatch();
        int counter = 0;

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

            stopwatch.Start();
            timer.Tick += Timer_Tick;
            timer.Start();

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

        private void Timer_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter % 100 == 0)
            {
                Debug.WriteLine("Avg tick time: " + (stopwatch.ElapsedMilliseconds / 100) + " ms");
                counter = 0;
                stopwatch.Restart();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cont = ((Button)sender).Content.ToString();
            uicontroller.Testf(cont);
        }
    }
}
