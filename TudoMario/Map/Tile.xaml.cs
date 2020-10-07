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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TudoMario.Map
{
    public sealed partial class Tile : UserControl
    {
        private string _ImagePath;
        public string ImagePath 
        {
            get { return _ImagePath; }
            set { ImageControl.Source = new BitmapImage(new Uri(value)); }
        }
        public Tile()
        {
            this.InitializeComponent();
            this.Width = 32;
            this.Height = 32;

            this.
            

            ImageControl.Width = 32;
            ImageControl.Height = 32;
        }


    }
}
